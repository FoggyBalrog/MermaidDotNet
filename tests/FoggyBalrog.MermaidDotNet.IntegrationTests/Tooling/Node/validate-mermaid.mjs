// What this file basically does is create a Node.js environment using JSDOM, load the Mermaid library, and then listen for diagram validation requests on stdin.
// The C# integration tests will spawn this script as a child process and communicate with it via stdin/stdout to validate Mermaid diagrams.

import readline from "node:readline";
import process from "node:process";
import { JSDOM } from "jsdom";

const dom = new JSDOM("<body></body>", {
  pretendToBeVisual: true,
  url: "http://localhost/",
});

const defineGlobalProperty = (name, value) => {
  Object.defineProperty(globalThis, name, {
    configurable: true,
    writable: true,
    value,
  });
};

defineGlobalProperty("window", dom.window);
defineGlobalProperty("document", dom.window.document);
defineGlobalProperty("navigator", dom.window.navigator);
defineGlobalProperty("Element", dom.window.Element);
defineGlobalProperty("HTMLElement", dom.window.HTMLElement);
defineGlobalProperty("SVGElement", dom.window.SVGElement);
defineGlobalProperty("Node", dom.window.Node);
defineGlobalProperty("DOMParser", dom.window.DOMParser);
defineGlobalProperty("Option", dom.window.Option);

try {
  const { default: mermaid } = await import("mermaid");

  mermaid.initialize({ startOnLoad: false });

  process.stdout.write(`${JSON.stringify({ ready: true })}\n`);

  const input = readline.createInterface({
    input: process.stdin,
    crlfDelay: Infinity,
    terminal: false,
  });

  // This will read lines from stdin until the stream is closed. Each line is expected to be a JSON string representing a validation request.
  for await (const line of input) {
    if (!line.trim()) {
      continue;
    }

    let request;

    try {
      request = JSON.parse(line);
    } catch (error) {
      process.stdout.write(
        `${JSON.stringify({
          ok: false,
          message: error instanceof Error ? error.stack ?? error.message : String(error),
        })}\n`,
      );
      continue;
    }

    const { id, diagram } = request;

    if (typeof diagram !== "string") {
      process.stdout.write(
        `${JSON.stringify({
          id,
          ok: false,
          message: "Worker request did not contain a string diagram payload.",
        })}\n`,
      );
      continue;
    }

    try {
      const parseResult = await mermaid.parse(diagram);

      if (parseResult === false) {
        process.stdout.write(
          `${JSON.stringify({
            id,
            ok: false,
            message: "Mermaid parse returned false.",
          })}\n`,
        );
        continue;
      }

      const diagramType =
        parseResult && typeof parseResult === "object" && "diagramType" in parseResult
          ? String(parseResult.diagramType)
          : "unknown";

      process.stdout.write(`${JSON.stringify({ id, ok: true, diagramType })}\n`);
    } catch (error) {
      const location =
        error && typeof error === "object" && "hash" in error
          ? error.hash?.loc
          : undefined;

      process.stdout.write(
        `${JSON.stringify({
          id,
          ok: false,
          location,
          message: error instanceof Error ? error.stack ?? error.message : String(error),
        })}\n`,
      );
    }
  }
} catch (error) {
  console.error(error instanceof Error ? error.stack ?? error.message : String(error));
  process.exit(1);
} finally {
  dom.window.close();
}
