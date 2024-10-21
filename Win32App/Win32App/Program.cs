using System.Runtime.InteropServices;

var handle = Metodlar.CreateFile(lpFileName: "C:\\Users\\tk\\Desktop\\Deneme.txt", dwDesiredAccess: 0x40000000, dwShareMode: 0, lpSecurityAttributes: IntPtr.Zero, dwCreationDisposition: 4, dwFlagsAndAttributes: 0, IntPtr.Zero);

string textToWrite = "Hello World";
var buffer = System.Text.Encoding.UTF8.GetBytes(textToWrite);
uint bytesWritten;
bool result = Metodlar.WriteFile(handle, buffer, (uint)buffer.Length,out bytesWritten, IntPtr.Zero);
if (!result)
    Console.WriteLine("Islem basarısız");
else
    Console.WriteLine("Basarili");

Metodlar.CloseHandle(handle);


public class Metodlar
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr CreateFile(
        //Açılmak istenen dosyanın tam yolunu ve adını alır
        string lpFileName,
        //Dosyaya erişim modunu belirler("GENERIC_READ": Dosya okuma izni, "GENERIC_WRITE" : Dosya yazma izni)
        uint dwDesiredAccess,
        //Dosyanın paylaşım modunu belirler, . Dosya açıldığında başka süreçlerin (uygulamaların) aynı dosyaya erişim şekli burada tanımlanır
        // "0": Dosya paylaşımına izin vermez,
        //"FILE_SHARE_READ", "FILE_SHARE_WRITE"
        uint dwShareMode,
        //dosya için güvenlik özelliklerini belirten bir yapıyı işaret eder, "IntPtr.Zero" : Varsayılan güvenlik  özellikleri.Güvenlik özellikleri, dosyaya kimlerin erişebileceğini ve dosya üzerinde hangi işlemleri yapabileceğini belirler.
        IntPtr lpSecurityAttributes,
        //Bu parametre, dosya oluşturma durumunu belirtir ve dosya üzerinde ne tür bir işlem yapılacağını tanımlar.
        // "CREATE_NEW" : Dosya yoksa oluşturur, varsa hata verir.(1)
        // "OPEN_EXISTING" : Var olan bir dosyayı açar, dosya yoksa hata verir.(3)
        // "CREATE_ALWAYS" : Her durumda dosyayı oluşturur varsa üstüne yazar.(2)
        // "OPEN_ALWAYS" : Dosya varsa açar, yoksa oluşturur. (4)
        uint dwCreationDisposition,
        //Bu parametre, dosyanın özelliklerini ve davranışını belirten bayrakları içerir.
        //"FILE_ATTRIBUTE_NORMAL": Normal bir dosya olduğunu belirtir.
        //"FILE_FLAG_OVERLAPPED" : Asenkron(eşzamansız) girdi / çıktı işlemleri için kullanılabilir.
        uint dwFlagsAndAttributes,
        //Bu parametre, yeni dosya için bir şablon dosyası belirtir. Genellikle kullanılmaz, bu nedenle IntPtr.Zero olarak bırakılır
        IntPtr hTemplateFile);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteFile(
        //Yazma işleminin yapılacağı dosya için açılmış olan dosya tanıtıcısını temsil eder. 
        IntPtr hFile,
        //Yazılacak olan veriyi içeren byte dizisidir. Yani dosyaya yazılacak verier burada depolanır.
        //Örneğin, bir metni UTF-8 olarak kodlayarak bu diziye yazabilir ve ardından dosyaya aktarabilirsiniz.
        byte[] lpBuffer,
        //Yazılacak veri miktarını (byte cinsinden) belirtir. Yani lpBuffer dizisinde kaç byte yazılacağını tanımlar. Bu değer 
        //lpBuffer dizisinin uzunluğuna eşit olmalıdır ya da daha küçük olabilir.
        uint nNumberofBytesToWrite,
        //Yazma işlemi bittikten sonra dosyaya kaç byte yazıldığını belirtmek için kullanılır.out anahtar kelimesi, bu parametrenin bir dönüş değeri değil, bir çıktı değeri olduğunu gösterir. Yazma işlemi başarıyla tamamlandığında, bu değişken belirtilen byte miktarını alır.
        out uint lpNumberOfBytesWritten,
        //Bu parametre, asenkron (eşzamansız) girdi/çıktı işlemleri için kullanılır. Eşzamansız işlemler yapıyorsanız, bu parametre OVERLAPPED yapısının adresini belirtir. Eğer asenkron yazma yapmıyorsanız, bu parametre genellikle IntPtr.Zero olarak ayarlanır.
        IntPtr lpOverlapped);
    [DllImport("kernel32", SetLastError = true)]
    public static extern bool CloseHandle(IntPtr hObject);
}