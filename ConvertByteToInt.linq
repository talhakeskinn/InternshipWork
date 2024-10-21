<Query Kind="Program" />

unsafe void Main()
{
	byte[] byteArray = new byte[]
	{
		150,
		07,
		00,
		00,
	};
	int c;
	byte* ptrC =(byte*)&c;
	for(int i = 0; i<sizeof(int); i++)
	{
		*ptrC = byteArray[i];
		ptrC++;
	}
	c.Dump();
	
}
