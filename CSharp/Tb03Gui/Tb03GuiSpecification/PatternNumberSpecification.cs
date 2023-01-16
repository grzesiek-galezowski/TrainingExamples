using Application.Ports;
using FluentAssertions;

namespace Tb03GuiSpecification;

public class Tests
{
  [TestCaseSource(nameof(FlatNumberToPatternNumberMappings))]
  public void ShouldCreateCorrectObjectsFromFlatNumbers(int flat, int groupNumber, int numberInGroup)
  {
    var fromFlatNumber = PatternNumber.FromFlatNumber(flat);
    fromFlatNumber.PatternGroupNumber.Should().Be(groupNumber);
    fromFlatNumber.PatternNumberInGroup.Should().Be(numberInGroup);
    fromFlatNumber.FlatPatternNumber.Should().Be(flat);
  }

  [TestCaseSource(nameof(FlatNumberToPatternNumberMappings))]
  public void ShouldCreateCorrectObjectsFromGroupAndPatternNumbers(int flat, int groupNumber, int numberInGroup)
  {
    var fromGroupAndNumberInGroup = PatternNumber.FromGroupAndNumberInGroup(groupNumber, numberInGroup);
    fromGroupAndNumberInGroup.PatternGroupNumber.Should().Be(groupNumber);
    fromGroupAndNumberInGroup.PatternNumberInGroup.Should().Be(numberInGroup);
    fromGroupAndNumberInGroup.FlatPatternNumber.Should().Be(flat);
  }

  public static object[][] FlatNumberToPatternNumberMappings()
  {
    return new object[][]
    {
      new object[] {0, 1, 1},
      new object[] {1, 1, 2},
      new object[] {2, 1, 3},
      new object[] {3, 1, 4},
      new object[] {4, 1, 5},
      new object[] {5, 1, 6},
      new object[] {6, 1, 7},
      new object[] {7, 1, 8},
      new object[] {8, 1, 9},
      new object[] {9, 1, 10},
      new object[] {10, 1, 11},
      new object[] {11, 1, 12},
      new object[] {12, 1, 13},
      new object[] {13, 1, 14},
      new object[] {14, 1, 15},
      new object[] {15, 1, 16},
      new object[] {16, 1, 17},
      new object[] {17, 1, 18},
      new object[] {18, 1, 19},
      new object[] {19, 1, 20},
      new object[] {20, 1, 21},
      new object[] {21, 1, 22},
      new object[] {22, 1, 23},
      new object[] {23, 1, 24},
      new object[] {24, 2, 1},
      new object[] {25, 2, 2},
      new object[] {47, 2, 24},
      new object[] {48, 3, 1},
      new object[] {71, 3, 24},
      new object[] {72, 4, 1},
      new object[] {95, 4, 24},
    };
  }
}