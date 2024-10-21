
using PermissionAttributeProject;
using System.Reflection;
public class Program
{
    private static void Main(string[] args)
    {

        Console.Write("Kullanıcı adını girin: ");
        var UserName = Console.ReadLine();

        Islem islem = new Ekle(12, 4);
        Islem islem2 = new CarpTopla(10, 5);
        Islem islem3 = new Bol(1000, 25);
        SonucFonksiyonu(islem, UserName);
        SonucFonksiyonu(islem2, UserName);

        Console.Write("---Bolme için isim: ");
        var UserName2 = Console.ReadLine();
        SonucFonksiyonu(islem3, UserName2);
    }
    static void SonucFonksiyonu(Islem islem, string UserName)
    {
        PermissionCheckResult check = new PermissionCheckResult();
        if (check.CheckPermission(islem, UserName))
        {
            Console.WriteLine(islem.Handle().ToString());
        }
        else
        {
            Console.WriteLine("İzin reddedildi.");
        }
    }
}

#region islemler
public abstract class Islem
{
    public int Sayi1 { get; set; }
    public int Sayi2 { get; set; }
    protected Islem(int Sayi1, int Sayi2)
    {
        this.Sayi1 = Sayi1;
        this.Sayi2 = Sayi2;
    }
    public abstract double Handle();
}
public class Ekle : Islem
{

    public Ekle(int Sayi1, int Sayi2) : base(Sayi1, Sayi2)
    {
    }

    [Permission("Ekle")]
    public override double Handle()
    {
        return Sayi1 + Sayi2;
    }
}
public class Cikar : Islem
{
    public Cikar(int Sayi1, int Sayi2) : base(Sayi1, Sayi2)
    {
    }

    [Permission("Cikar")]
    public override double Handle()
    {
        return Sayi1 - Sayi2;
    }
}
public class Carp : Islem
{

    public Carp(int Sayi1, int Sayi2) : base(Sayi1, Sayi2)
    {
    }

    [Permission("Carp")]
    public override double Handle()
    {
        return Sayi1 * Sayi2;
    }
}
public class Bol : Islem
{
    public Bol(int Sayi1, int Sayi2) : base(Sayi1, Sayi2)
    {
    }

    [Permission("Bol")]
    public override double Handle()
    {
        return Sayi1 / Sayi2;
    }
}
public class CarpTopla : Islem
{
    public CarpTopla(int Sayi1, int Sayi2) : base(Sayi1, Sayi2)
    {
    }
    [Permission("Ekle")]
    public override double Handle()
    {
        return Sayi1 * Sayi2 + Sayi1 + Sayi2;
    }
}

#endregion

public class PermissionCheckResult
{
    public bool CheckPermission(Islem islem, string UserName)
    {
        var type = islem.GetType();
        var method = type.GetMethod("Handle");
        var attribute = method.GetCustomAttribute(typeof(PermissionAttribute));
        if (attribute != null)
        {
            var attType = attribute.GetType().GetProperty("CommandName");
            string _commandName = attType.GetValue(attribute).ToString();

            if (_commandName != null)
                return new Data().ReturnPermissionResult(_commandName, UserName);

            return false;
        }

        return false;


    }
}
