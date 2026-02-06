#!/usr/bin/env bash
set -euo pipefail

README="README.md"
INDEX="docs/index.md"
PLACEHOLDER="<!-- Quick Start Placeholder -->"

# Extract the "## Quick Start" section from README.md (up to, but not including, the next H2)
qs_tmp="$(mktemp)"
trap 'rm -f "$qs_tmp"' EXIT

awk '
  BEGIN { in=0 }
  # Start when we hit the exact H2 title "## Quick Start"
  /^##[[:space:]]+Quick Start[[:space:]]*$/ { in=1; print; next }
  # Stop at the next H2 once we are inside the section
  in && /^##[[:space:]]+/ { exit }
  # Print lines while inside the section
  in { print }
' "$README" > "$qs_tmp"

# Check if section found
if ! grep -qE '^##[[:space:]]+Quick Start([[:space:]]*)$' "$qs_tmp"; then
  echo "Error: Could not find a '## Quick Start' section in $README" >&2
  exit 1
fi

# Replace "./docs" with "~" inside the extracted section
perl -pe 's/\Q.\/docs\E/~/g' -i "$qs_tmp"

# Replace the placeholder in docs/index.md
perl -0777 -i -pe '
  my $ph = $ENV{PH};
  my $qs_path = $ENV{QS};

  open my $fh, "<", $qs_path or die "Cannot open extracted section: $qs_path ($!)";
  local $/;
  my $qs = <$fh>;
  close $fh;

  die "Error: placeholder not found in index file\n" if index($_, $ph) < 0;

  $_ =~ s/\Q$ph\E/$qs/g;
' "$INDEX" \
  PH="$PLACEHOLDER" QS="$qs_tmp"

echo "Updated
