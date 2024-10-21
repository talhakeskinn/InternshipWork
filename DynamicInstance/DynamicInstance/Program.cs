/* CSharpCodeProvider dinamik olarak C# kodu oluşturmak ve bu kodu derlemek için kullanılır.

CompilerParameters Parametreleri: 
-GenerateExecutable : Derlenen kodun bir çalıştırılabilir dosya (EXE) oluşturup oluşturmayacağını belirtir, False ise Dll üretir.
-OutputAssembly : Derlenen kodun kaydedileceği dosya adı ve yolu. Bu, derleme biriminin (örneğin, EXE veya DLL) adını ve konumunu belirler.
-GenerateInMemory: Derlenen derleme biriminin bellek içi olarak oluşturulup oluşturulmayacağını belirtir. Eğer true ise, derleme birimi diske
yazılmaz ve doğrudan bellek içinde tutulur. Bu genellikle derlemeyi hemen yüklemek ve çalıştırmak için kullanılır.
*/

/* CompileAssemblyFromFile: Bu yöntem, kaynak kodunu bir dosyadan alır ve derler.*/

/* CompileAssemblyFromSource(Direkt kaynak kodu veriyorsun) ve CompileAssemblyFromDom(CompileUnit türünde bir nesne alır.
Bu nesne, derlenecek kodun tüm yapısını ve içeriğini içerir.),CodeDomProvider sınıfının iki farklı yöntemidir ve her biri farklı türdeki
giriş verilerini kullanarak kodu derler.*/

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

string sourceCode = @"
namespace MyNamespace
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}";

#region Propertylere erişim
SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
SyntaxNode root = syntaxTree.GetRoot();
var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();
var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
var properties = root.DescendantNodes().OfType<PropertyDeclarationSyntax>();

// Product sınıfının örneğini oluştur
foreach (var c in classes)
{
    // Sınıfın adı ve namespace
    var className = $"MyNamespace.{c.Identifier.Text}"; // Tam adı oluştur

    // Type.GetType ile sınıfın tipini al
    var productType = Type.GetType(className);
    if (productType == null)
    {
        Console.WriteLine($"Type '{className}' could not be found.");
        continue; // Devam et
    }

    // Activator ile sınıfın örneğini oluştur
    var productInstance = Activator.CreateInstance(productType);

    Console.WriteLine($"Created instance of {className}");
}

Console.WriteLine("Methods...");
foreach (var method in methods)
{
    Console.WriteLine(method.ToString());
}

Console.WriteLine("*********\nProperties...");
foreach (var property in properties)
{
    Console.WriteLine(property.ToString());
}
#endregion

Console.Read();