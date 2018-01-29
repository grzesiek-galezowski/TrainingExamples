00 - polymorphism -> separate use from construction
01 - context independence
02 - describe this example!!
   - problem - refactoring inside out - leads to composition root (see 03)
03 - what is composition root (not a single class)
   - dependencies of composition root(CR dependent on details)
   - How to make composition root smaller/more readable?
   - pure DI + DSL (show examples)
   - vs IoC (philosophy difference)
   - IoC containers basics
     - what IoC containers do (slide)
     - examples, Pico (manual registration), Spring (scanning)
     - reflection vs XML vs annotation processing
     - RRR pattern (register resolve release)
     - resource management
     - scopes
     - philosophy
     - IoC place in composition root
     - no dependency on IoC container
   - issues with IoC containers:
     - learn new interface (falls short in case
     - usually runtime resolution (circular dependencies)
   - IoC as a frameworks integration tool

---
04 - bastard injection antipattern
   - does not decouple
   - single constructor principle (show confluence page)
05 - service locator
06 - conforming container