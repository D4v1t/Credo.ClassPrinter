using Credo.ClassPrinter.BusinessLogic.Services;
using Credo.ClassPrinter.DataContracts.Constants;
using Credo.ClassPrinter.Models;

[TestFixture]
public class TypeInspectorTests
{
    private RecursiveObjectPrinter _printer;

    [SetUp]
    public void Setup()
    {
        _printer = new RecursiveObjectPrinter();
    }

    [Test]
    public void Print_ShouldHandleSimpleType()
    {
        // Arrange
        var type = typeof(ClassWithProperties);

        // Act
        var result = _printer.Print(type);

        // Assert
        StringAssert.Contains($"{MetadataConstants.ClassLabel}: {nameof(ClassWithProperties)}", result);
    }

    [Test]
    public void Print_ShouldHandleTypeWithProperties()
    {
        // Arrange
        var type = typeof(ClassWithProperties);

        // Act
        var result = _printer.Print(type);

        // Assert
        StringAssert.Contains($"{MetadataConstants.ClassLabel}: {nameof(ClassWithProperties)}", result);
        StringAssert.Contains($"{MetadataConstants.PropertyLabel}: {nameof(ClassWithProperties.StringProperty)} (Type: String)", result);
        StringAssert.Contains($"{MetadataConstants.PropertyLabel}: {nameof(ClassWithProperties.IntegerProperty)}", result);
    }

    [Test]
    public void Print_ShouldHandleNestedTypes()
    {
        // Arrange
        var type = typeof(ClassWithPropertiesAndNestedTypes);

        // Act
        var result = _printer.Print(type);

        // Assert
        StringAssert.Contains($"{MetadataConstants.ClassLabel}: {nameof(NestedClass)}", result);
    }

    [Test]
    public void Print_ShouldHandleCircularReferences()
    {
        // Arrange
        var type = typeof(CircularClass);

        // Act
        var result = _printer.Print(type);

        // Assert
        StringAssert.Contains($"{MetadataConstants.ClassLabel}: {nameof(CircularClass)}", result);
        StringAssert.Contains(BusinessErrorMessages.CirucalReferenceDetected, result);
    }
}
