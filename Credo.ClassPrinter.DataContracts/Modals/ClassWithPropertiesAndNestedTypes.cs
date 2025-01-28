namespace Credo.ClassPrinter.Models
{
    public class ClassWithPropertiesAndNestedTypes : ClassWithProperties
    {
        public NestedClass NestedClass { get; set; }

        public IEnumerable<NestedClass> NestedClasses { get; set; }
    }
}
