#!/usr/bin/env bash
set -euo pipefail

DIR="docs/diagrams"
OUT="$DIR/toc.yml"

: > "$OUT"

for f in "$DIR"/*.md; do
  base="$(basename "$f")"

  # First H1, remove HTML comments on that line, trim
  title="$(grep -m1 -E '^# ' "$f" \
    | sed -E 's/^# +//; s/<!--.*-->//g; s/[[:space:]]+$//')"

  [[ -n "${title:-}" ]] || title="${base%.md}"

  printf -- "- name: %s\n  href: %s\n" "$title" "$base" >> "$OUT"
done

echo "Wrote $OUT"
