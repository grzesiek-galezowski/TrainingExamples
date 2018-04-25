package immutability;

import lombok.val;

import java.util.Arrays;
import java.util.List;

public class _01_ImmutableReferences {

    public static void main(String[] args) {
        mutablePrimitive();
        immutablePrimitive();
        shadowingPrimitive();
        mutableReferenceAndValue();
        immutableReferenceMutableValue();
        immutableReferenceAndValue();
        immutableReferenceAndValueWithVal();
    }


    private static void mutablePrimitive() {
        ///mutable
        int x1 = 23;
        x1 = 24;
    }

    private static void immutablePrimitive() {
        //immutable
        final int x2 = 23;
        //x2 = 24; //won't compile
    }

    private static void shadowingPrimitive() {
        //immutable
        val x2 = 23;
        //val x2 = "alamakota"; shadowing/rebinding. Not supported in Java
    }


    private static void mutableReferenceAndValue() {
        List<Integer> list = Arrays.asList(1,2,3,4,5);
        //mutable reference
        list = Arrays.asList(2,3,4,5,6);
        //mutable value
        list.add(123);
    }

    private static void immutableReferenceMutableValue() {
        final List<Integer> list2 = Arrays.asList(1,2,3,4,5);
        //immutable reference
        //list2 = Arrays.asList(1,2,3,4,5); //won't compile
        //mutable value
        list2.add(123);
    }

    private static void immutableReferenceAndValue() {
        final io.vavr.collection.List<Integer> list3
            = io.vavr.collection.List.of(1,2,3,4,5);
        //immutable reference
        //list3 = io.vavr.collection.List.of(1,2,3,4,5); //won't compile
        //immutable value
        final io.vavr.collection.List<Integer> list4 = list3.append(123);
    }

    private static void immutableReferenceAndValueWithVal() {
        val list3
            = io.vavr.collection.List.of(1,2,3,4,5);
        //immutable reference
        //list3 = io.vavr.collection.List.of(1,2,3,4,5); //won't compile
        //immutable value
        val list4 = list3.append(123);
    }

}
