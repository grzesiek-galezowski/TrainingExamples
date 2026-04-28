# Copilot Instructions

## Project Guidelines
- For this MutaSynth workspace, prefer Windows-only targeting for now and avoid enabling Android builds unless explicitly requested.

### Synth Parameter Semantics
- Use analog-style semantics for synth parameter scales.
- Treat the maximum value 128.0 as fully on: filter cutoff = 128.0 means fully open.
- Treat resonance = 128.0 as strong self-oscillation.
- Preserve these semantics across UI, automation, and internal mappings (map, clamp, and document accordingly).