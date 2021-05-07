Potential pros of automocking:

1. Decouples constructor from test (can be done better using builders)
2. Makes test a bit shorter in a simple case

Cons:
1. Hides bloated constructor smell
2. Obfuscates readability
3. Turns pretty normal use cases into edge cases (which either further obfuscates readability or forces to rely on manual composition anyway)

TODO: Fixture object example
TODO: Builder example