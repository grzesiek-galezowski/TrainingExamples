using FluentAssertions;

namespace Tests;

public class Tests
{

  // Obvious case: no movement
  [TestCase(new[] { 1 }, 0, 0, 0, new[] { 1 })] //No movement needed - zero offset and segment length

  // Basic Segment Movement - Small Arrays
  [TestCase(new[] { 1, 2, 3 }, 0, 3, 1, new[] { 3, 1, 2 })] //Basic right rotation of entire array
  [TestCase(new[] { 1, 2 }, 0, 2, 2, new[] { 1, 2 })] //Multiple rotation by 2 returns to original state

  // Multiple Segments
  [TestCase(new[] { 1, 2, 3, 4 }, 0, 2, 1, new[] { 2, 1, 4, 3 })] //Two segments of length 2, each rotated right

  // Negative Offset Cases
  [TestCase(new[] { 1, 2, 3 }, 0, 3, -1, new[] { 2, 3, 1 })] //Left rotation of entire array
  [TestCase(new[] { 1, 2, 3 }, -1, 2, -1, new[] { 3, 2, 1 })] //Negative start index with negative offset

  // Complex Cases - Large Arrays with Wrapping
  [TestCase(new[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }, -9, 5, -7,
    new[] { 20, 13, 12, 16, 17, 18, 14, 15, 21, 22, 11, 19 })] //Large array with negative start index and offset
  [TestCase(new[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }, 3, 5, -7,
    new[] { 20, 13, 12, 16, 17, 18, 14, 15, 21, 22, 11, 19 })] //Large array with positive start index and negative offset

  // Edge Cases - moving a single element
  [TestCase(new[] { 1 }, 0, 1, 1, new[] { 1 })] //Single element rotation - result unchanged regardless of offset
  [TestCase(new[] { 1 }, 0, -1, 1, new[] { 1 })] //Single element rotation - result unchanged regardless of offset
  [TestCase(new[] { 1 }, 0, 1, -1, new[] { 1 })] //Single element rotation - result unchanged regardless of offset
  [TestCase(new[] { 1 }, 1, 1, 1, new[] { 1 })] //Single element rotation - result unchanged regardless of offset

  // Edge case - segment larger than array
  [TestCase(new[] { 1, 2 }, 0, 3, 1, new[] { 2, 1 })] //Right rotation with segment length > array length

  public void Test1(int[] input, int firstSegmentStartIndex, int segmentLength, int offset, int[] expected)
  {
    MoveElements2(input, firstSegmentStartIndex, segmentLength, offset);

    input.Should().Equal(expected);
  }

  static void MoveElements(int[] array, int firstSegmentStartIndex, int segmentLength, int offset)
  {
    if (firstSegmentStartIndex < 0)
    {
      MoveRight(array, 0, array.Length, -firstSegmentStartIndex);
    }
    else if (firstSegmentStartIndex > 0)
    {
      MoveLeft(array, 0, array.Length, -firstSegmentStartIndex);
    }

    if (offset > 0)
    {
      MoveRight(array, 0, segmentLength, offset);
    }
    else
    {
      MoveLeft(array, 0, segmentLength, offset);
    }

    if (firstSegmentStartIndex < 0)
    {
      MoveLeft(array, 0, array.Length, firstSegmentStartIndex);
    }
    else if (firstSegmentStartIndex > 0)
    {
      MoveRight(array, 0, array.Length, firstSegmentStartIndex);
    }
  }

  private static void MoveRight(int[] array, int firstSegmentStartIndex, int segmentLength, int offset)
  {
    for (int i = 0; i < offset; i++)
    {
      for (int segmentStart = firstSegmentStartIndex; segmentStart < array.Length; segmentStart += segmentLength)
      {
        int segmentEnd = Math.Min(segmentStart + segmentLength, array.Length);
        int lastElement = array[segmentEnd - 1];

        for (int j = segmentEnd - 1; j > segmentStart; j--)
        {
          array[j] = array[j - 1];
        }

        array[segmentStart] = lastElement;
      }
    }
  }

  private static void MoveLeft(int[] array, int firstSegmentStartIndex, int segmentLength, int offset)
  {
    offset = -offset;
    for (int i = 0; i < offset; i++)
    {
      for (int segmentStart = firstSegmentStartIndex; segmentStart < array.Length; segmentStart += segmentLength)
      {
        int segmentEnd = Math.Min(segmentStart + segmentLength, array.Length);
        int firstElement = array[segmentStart];

        for (int j = segmentStart; j < segmentEnd - 1; j++)
        {
          array[j] = array[j + 1];
        }

        array[segmentEnd - 1] = firstElement;
      }
    }
  }

  static void MoveElements2(int[] array, int firstSegmentStartIndex, int segmentLength, int offset)
  {
    if(array.Length <= 1)
    {
      return;
    }

    int shiftArrayBound = firstSegmentStartIndex < 0 ? -firstSegmentStartIndex : firstSegmentStartIndex;

    if (firstSegmentStartIndex < 0)
    {
      for (int i = 0; i < shiftArrayBound; i++)
      {
        int lastElement = array[array.Length - 1];

        for (int j = array.Length - 1; j > 0; j--)
        {
          array[j] = array[j - 1];
        }

        array[0] = lastElement;
      }
    }
    else if (firstSegmentStartIndex > 0)
    {
      for (int i = 0; i < shiftArrayBound; i++)
      {
        int firstElement = array[0];

        for (int j = 0; j < array.Length - 1; j++)
        {
          array[j] = array[j + 1];
        }

        array[array.Length - 1] = firstElement;
      }
    }

    if (offset > 0)
    {
      for (int i = 0; i < offset; i++)
      {
        for (int segmentStart = 0; segmentStart < array.Length; segmentStart += segmentLength)
        {
          int segmentEnd = Math.Min(segmentStart + segmentLength, array.Length);
          int lastElement = array[segmentEnd - 1];

          for (int j = segmentEnd - 1; j > segmentStart; j--)
          {
            array[j] = array[j - 1];
          }

          array[segmentStart] = lastElement;
        }
      }
    }
    else
    {
      for (int i = 0; i < -offset; i++)
      {
        for (int segmentStart = 0; segmentStart < array.Length; segmentStart += segmentLength)
        {
          int segmentEnd = Math.Min(segmentStart + segmentLength, array.Length);
          int firstElement = array[segmentStart];

          for (int j = segmentStart; j < segmentEnd - 1; j++)
          {
            array[j] = array[j + 1];
          }

          array[segmentEnd - 1] = firstElement;
        }
      }
    }

    if (firstSegmentStartIndex < 0)
    {
      for (int i = 0; i < shiftArrayBound; i++)
      {
        int firstElement = array[0];

        for (int j = 0; j < array.Length - 1; j++)
        {
          array[j] = array[j + 1];
        }

        array[array.Length - 1] = firstElement;
      }
    }
    else if (firstSegmentStartIndex > 0)
    {
      for (int i = 0; i < shiftArrayBound; i++)
      {
        int lastElement = array[array.Length - 1];

        for (int j = array.Length - 1; j > 0; j--)
        {
          array[j] = array[j - 1];
        }

        array[0] = lastElement;
      }
    }
  }

}