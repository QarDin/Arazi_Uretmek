
using Raylib_cs;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;

Camera3D cam = new Camera3D();
cam.Position = new Vector3(0.0f, 10.0f, 10.0f);
cam.Target = new Vector3(0.0f, 0.0f, 0.0f);
cam.Up = new Vector3(0.0f, 1.0f, 0.0f);
cam.FovY = 45.0f;
cam.Projection = CameraProjection.Perspective;
Raylib.SetTargetFPS(60);
Raylib.InitWindow(800, 600, "Arazi_Denemesi");
Raylib.DisableCursor();

#region Random Sayi ureten yer
Random rnd = new Random();

#endregion
#region Degiskenlerin dizilerin tanimlandigi bolum.
int RowDot = 11;
int ColmunDot = 11;
int holdValue = 0;
float[,] fieldValue = new float[RowDot, ColmunDot];
int counter = 0;
Vector3[,] vectors = new Vector3[RowDot, ColmunDot];
int aralik = 5;
bool noiseKullan = true; // false = Halka 2 (+-1 kurali)
#endregion
//  10x10 arazi 
#region Hash isleminin yapildigi parca
static uint hash(uint x)
{
    x = (x >> 16) ^ x * 0x45d9f3bu;
    x = ((x >> 16) ^ x) * 0x45d9f3bu;
    x = (x >> 16) ^ x;
    return x;
}
static double noise(int c, int aralik)
{
    int holder = c / aralik;
    double oran = (double)(1.0 / aralik);
    double sonuc = oran * (c % aralik);
    double donusturucu = sonuc * sonuc * (3 - 2 * sonuc);
    int a = (int)hash((uint)holder) % 20;
    int b = (int)hash((uint)(holder + 1)) % 20;
    double fark = b - a;
    double adim = fark * donusturucu;
    return a + adim;
}
double noise2D(int i, int j, int aralik)
{
    int ni = i / aralik;
    int nj = j / aralik;
    int solUst = (int)hash((uint)(ni * 100 + nj)) % 20;
    int sagUst = (int)hash((uint)(ni * 100 + nj + 1)) % 20;
    int solAlt = (int)hash((uint)((ni + 1) * 100 + nj)) % 20;
    int sagAlt = (int)hash((uint)((ni + 1) * 100 + nj + 1)) % 20;
    double satirKalani = i % aralik;
    double stunKalani = j % aralik;
    double satirOrani = satirKalani / (double)aralik;
    double stunOrani = stunKalani / (double)aralik;
    double satirS = satirOrani * satirOrani * (3 - 2 * satirOrani);
    double stunS = stunOrani * stunOrani * (3 - 2 * stunOrani);
    double fark = sagUst - solUst;
    double adim = fark * stunS;
    double ust = solUst + adim;
    double sagAltAdim = sagAlt - solAlt;
    double sagAltFark = sagAltAdim * stunS;
    double alt = solAlt + sagAltFark;
    double dikeyFark = alt - ust;
    double dikeyAdim = dikeyFark * satirS;
    return ust + dikeyAdim;

}
// for (int k = 0; k < 10; k++)
// {
//     var val = hash((uint)k);
//     //System.Console.WriteLine(val % 20);

// }
#endregion
#region araziye degere atayan dongu 
for (int i = 0; i < RowDot; i++)
{
    //System.Console.WriteLine(i);
    for (int j = 0; j < ColmunDot; j++)
    {
        if (noiseKullan)
        {
            fieldValue[i, j] = (float)noise2D(i, j, aralik);
            vectors[i, j] = new Vector3(j - 5, fieldValue[i, j] - 5, i - 5);
            continue;
        }
        if (j == 0 && i == 0)
        {
            holdValue = rnd.Next(0, 20);
            fieldValue[i, j] = holdValue;

        }
        else if (i == 0)
        {


            holdValue = rnd.Next((int)fieldValue[i, j - 1] - 1, (int)fieldValue[i, j - 1] + 2);


        }
        else if (j == 0 && i > 0)
        {
            holdValue = rnd.Next((int)fieldValue[i - 1, j] - 1, (int)fieldValue[i - 1, j] + 2);
        }
        else
        {
            float enAz = Math.Max(fieldValue[i, j - 1] - 1, fieldValue[i - 1, j] - 1);
            float enFazla = Math.Min(fieldValue[i, j - 1] + 1, fieldValue[i - 1, j] + 1);
            holdValue = rnd.Next((int)enAz, (int)enFazla + 1);
        }
        fieldValue[i, j] = holdValue;
        vectors[i, j] = new Vector3(j - 5, fieldValue[i, j] - 5, i - 5);
        counter++;
    }
}
for (int i = 0; i < RowDot - 1; i++)
{
    //System.Console.WriteLine(i);
    for (int j = 0; j < ColmunDot - 1; j++)
    {
        System.Console.WriteLine($"{fieldValue[i, j]},{fieldValue[i + 1, j]},{fieldValue[i, j + 1]},{fieldValue[i + 1, j + 1]} -------- {counter}");

        counter++;
    }
}
#endregion
#region 

#endregion
#region Cizimlerin gerceklestigi alan.
while (!Raylib.WindowShouldClose())
{
    Raylib.UpdateCamera(ref cam, CameraMode.Free);

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.DarkBlue);

    Raylib.BeginMode3D(cam);

    Raylib.DrawGrid(10, 1.0f);
    for (int i = 0; i < RowDot - 1; i++)
    {
        //System.Console.WriteLine(i);
        for (int j = 0; j < ColmunDot - 1; j++)
        {
            int valueHolder = rnd.Next(0, 20);


            Raylib.DrawTriangle3D(vectors[i, j], vectors[i + 1, j], vectors[i, j + 1], Color.DarkGreen);
            Raylib.DrawTriangle3D(vectors[i + 1, j + 1], vectors[i, j + 1], vectors[i + 1, j], Color.Green);
            counter++;
        }
    }

    Raylib.EndMode3D();

    Raylib.EndDrawing();
}
Raylib.CloseWindow();
#endregion