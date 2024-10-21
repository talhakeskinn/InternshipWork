using System.Reflection;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Threading.Channels;

public class Program
{
    private static void Main(string[] args)
    {
        Deneme deneme = new();
        deneme.Yazdir();
        Deneme5 deneme5 = new();
        deneme5.yazdir2();
        #region YazdirmaFonks
        void listeYazdir(List<Type> liste)
        {
            foreach (var type in liste)
            {
                Console.WriteLine(type.Name.ToString());
            }
        }
        void MethodYazdir(List<MethodInfo> liste)
        {
            foreach (var method in liste)
            {
                Console.WriteLine(method.Name.ToString());
            }
        }
        #endregion
        #region Attribute erişim

        Type classType = typeof(Deneme);
        var classAttribute = (DescAttribute)Attribute.GetCustomAttribute(classType, typeof(DescAttribute));
        if (classAttribute != null)
        {
            Console.WriteLine($"{classAttribute.Description}");
        }

        var methodInfo = classType.GetMethod("Yazdir");
        var methodAttribute = (DescAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(DescAttribute));

        if (methodAttribute != null)
        {
            Console.WriteLine($"{methodAttribute.Description}");
        }

        #endregion

        Assembly assembly = Assembly.GetExecutingAssembly(); //Çalışan Geçerli assembly aldık 
        var assemblyType = assembly.GetTypes();
        var classWithAttribute = new List<Type>();
        var methodWithAttribute = new List<MethodInfo>();
        foreach (var type in assemblyType)
                                                                                                                                                                                                                                             
        {
            if (Attribute.IsDefined(type, typeof(DescAttribute)))
                classWithAttribute.Add(type);
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var method in methods)
                if (Attribute.IsDefined(method, typeof(DescAttribute)))
                    methodWithAttribute.Add(method);
        }
        Console.WriteLine("Attribute bulunan siniflar"); listeYazdir(classWithAttribute);

        Console.WriteLine("Attribute bulunan metodlar"); MethodYazdir(methodWithAttribute);

    }
    [Desc("Bu bir siniftir")]
    public class Deneme
    {
        [Desc("Bu bir metoddur")]
        public void Yazdir()
        {
            Console.WriteLine("Selam \n");
        }
    }
    [Desc("Bu bir sinif")]
    public class Deneme2 { }
    public class Deneme3 { }

    [Desc("Bu bir sinif")]
    public class Deneme4 { }
    public class Deneme5
    {
        [Desc("Bu bir method")]
        public void yazdir2()
        { Console.WriteLine("fener"); }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DescAttribute : Attribute
    {
        public string Description { get; set; }
        public DescAttribute(string Description)
        {
            this.Description = Description;
        }
    }
}