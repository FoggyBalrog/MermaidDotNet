#!/usr/bin/env bash
set -euo pipefail

README="README.md"
INDEX="docs/index.md"
PLACEHOLDER="<!-- Quick Start Placeholder -->"

qs_tmp="$(mktemp)"
trap 'rm -f "$qs_tmp"' EXIT

# Extract "## Quick Start" section (until next H2)
awk '
  BEGIN { in_section=0 }
  /^##[[:space:]]+Quick Start[[:space:]]*$/ { in_section=1; print; next }
  in_section && /^##[[:space:]]+/ { exit }
  in_section { print }
' "$README" > "$qs_tmp"

# Sanity check
if ! grep -qE '^##[[:space:]]+Quick Start([[:space:]]*)$' "$qs_tmp"; then
  echo "Error: Could not find a '## Quick Start' section in $README" >&2
  exit 1
fi

# Replace literal "./docs" with "~" inside the extracted section
perl -pe 's/\Q.\/docs\E/~/g' -i "$qs_tmp"

# Replace placeholder in docs/index.md with extracted section
PH="$PLACEHOLDER" QS="$qs_tmp" perl -0777 -i -pe '
  my $ph = $ENV{PH} // die "PH not set\n";
  my $qs_path = $ENV{QS} // die "QS not set\n";

  open my $fh, "<", $qs_path or die "Cannot open extracted section: $qs_path ($!)\n";
  local $/;
  my $qs = <$fh>;
  close $fh;

  die "Error: placeholder not found in index file\n" if index($_, $ph) < 0;

  s/\Q$ph\E/$qs/g;
' "$INDEX"

echo "Updated $INDEX"
