package logic;

import autofixture.interfaces.InlineInstanceGenerator;
import autofixture.publicinterface.Any;

public class InlineGenerators {
    static InlineInstanceGenerator<FileName> fileName() {
        return fixture -> FileName.from(Any.alphaString());
    }
}
