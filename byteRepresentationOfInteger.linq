<Query Kind="Program" />


/* -- C#'ta göstericiler yerine referans değişkenleri mevcuttur. Referanslar heap bellek bölgesindeki nesnelerin başlangıç adresini tutar.
Ancak bu adresin değerine kesinlikle ulaşamayız. -- */

/* 1907 değeri ikili tabanda en yüksek bayt <-- ...0000 0111 0111 0011 --> En düşük bayt, Bellekte en düşük bayttan ek yüksek bayta doğru 
tutulur. */

unsafe void Main()
{
	int x = 1907;
	
	byte* ptbX = (byte*)&x;
	
	  for (int i = 0; i < sizeof(int); i++)
	{
		byte byteValue = *(ptbX + i);
		string binaryValue = Convert.ToString(value: byteValue, toBase:2).PadLeft(totalWidth:8,'0');
		string decimalValue = byteValue.ToString();
		string hexValue = byteValue.ToString(format:"X2");
		Console.WriteLine($"Bayt {i + 1}: Binary: {binaryValue}, Decimal: {decimalValue}, Hexadecimal: {hexValue}");
	}
}


