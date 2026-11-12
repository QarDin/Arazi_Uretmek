
using Raylib_cs;
using System.Numerics;


Camera3D cam = new Camera3D();
cam.Position = new Vector3(0.0f, 10.0f, 10.0f);
cam.Target = new Vector3(0.0f, 0.0f, 0.0f);
cam.Up = new Vector3(0.0f, 1.0f, 0.0f);
cam.FovY = 45.0f;
cam.Projection = CameraProjection.Perspective;
Raylib.SetTargetFPS(0);
//Raylib.SetConfigFlags(ConfigFlags.FullscreenMode);
Raylib.InitWindow(1920, 1080, "Arazi_Denemesi");
Raylib.DisableCursor();



Random rnd = new Random();


int RowDot = 110;
int ColmunDot = 110;
int holdValue = 0;
float[,] fieldValue = new float[RowDot, ColmunDot];
int counter = 0;
Vector3[,] vectors = new Vector3[RowDot, ColmunDot];
int aralik = 5;
int aralikKucuk = 3;
bool noiseKullan = true;

ushort[] ucgenNumaralari = new ushort[(RowDot - 1) * (ColmunDot - 1) * 6];
float[] ucgenNoktalari = new float[RowDot * ColmunDot * 3];
int tabloBoyu = (RowDot - 1) / aralik + 2;
int[,] kontrol = new int[tabloBoyu, tabloBoyu];
for (int t = 0; t < tabloBoyu; t++)
    for (int u = 0; u < tabloBoyu; u++)
        kontrol[t, u] = rnd.Next(10);

int tabloBoyuKucuk = (RowDot - 1) / aralikKucuk + 2;
int[,] kontrolKucuk = new int[tabloBoyuKucuk, tabloBoyuKucuk];
for (int t = 0; t < tabloBoyuKucuk; t++)
    for (int u = 0; u < tabloBoyuKucuk; u++)
        kontrolKucuk[t, u] = rnd.Next(2);

double noise2D(int i, int j, int aralik, int[,] tablo)
{
    int ni = i / aralik;
    int nj = j / aralik;
    int solUst = tablo[ni, nj];
    int sagUst = tablo[ni, nj + 1];
    int solAlt = tablo[ni + 1, nj];
    int sagAlt = tablo[ni + 1, nj + 1];
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

for (int i = 0; i < RowDot; i++)
{
    //System.Console.WriteLine(i);
    for (int j = 0; j < ColmunDot; j++)
    {
        if (noiseKullan)
        {
            fieldValue[i, j] = (float)noise2D(i, j, aralik, kontrol) + (float)noise2D(i, j, aralikKucuk, kontrolKucuk);
            vectors[i, j] = new Vector3(j - 50, fieldValue[i, j] - 50, i - 50);
            continue;
        }
        if (j == 0 && i == 0)
        {
            holdValue = rnd.Next(0, 10);
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
for (int i = 0; i < RowDot; i++)
{

    for (int a = 0; a < ColmunDot; a++)
    {

        ucgenNoktalari[(i * ColmunDot + a) * 3] = vectors[i, a].X;
        ucgenNoktalari[(i * ColmunDot + a) * 3 + 1] = vectors[i, a].Y;
        ucgenNoktalari[(i * ColmunDot + a) * 3 + 2] = vectors[i, a].Z;

    }
}
int sira = 0;
for (int j = 0; j < RowDot - 1; j++)
{

    for (int k = 0; k < ColmunDot - 1; k++)
    {
        int solUst = j * ColmunDot + k;
        int sagUst = j * ColmunDot + k + 1;
        int solAlt = (j + 1) * ColmunDot + k;
        int sagAlt = (j + 1) * ColmunDot + k + 1;
        ucgenNumaralari[sira] = (ushort)solUst;
        sira++;
        ucgenNumaralari[sira] = (ushort)solAlt;
        sira++;
        ucgenNumaralari[sira] = (ushort)sagUst;
        sira++;
        ucgenNumaralari[sira] = (ushort)sagAlt;
        sira++;
        ucgenNumaralari[sira] = (ushort)sagUst;
        sira++;
        ucgenNumaralari[sira] = (ushort)solAlt;
        sira++;

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
Mesh arazi = new Mesh();
arazi.VertexCount = RowDot * ColmunDot;
arazi.TriangleCount = (RowDot - 1) * (ColmunDot - 1) * 2;
arazi.AllocVertices();
arazi.AllocIndices();
ucgenNoktalari.CopyTo(arazi.VerticesAs<float>());
ucgenNumaralari.CopyTo(arazi.IndicesAs<ushort>());
Raylib.UploadMesh(ref arazi, false);
var loadMaterial = Raylib.LoadMaterialDefault();

while (!Raylib.WindowShouldClose())
{
    Raylib.UpdateCamera(ref cam, CameraMode.Free);

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.DarkBlue);

    Raylib.BeginMode3D(cam);

    Raylib.DrawGrid(100, 1.0f);
    // for (int i = 0; i < RowDot - 1; i++)
    // {
    //     //System.Console.WriteLine(i);
    //     for (int j = 0; j < ColmunDot - 1; j++)
    //     {
    //         int valueHolder = rnd.Next(0, 20);


    //         Raylib.DrawTriangle3D(vectors[i, j], vectors[i + 1, j], vectors[i, j + 1], Color.DarkGreen);
    //         Raylib.DrawTriangle3D(vectors[i + 1, j + 1], vectors[i, j + 1], vectors[i + 1, j], Color.Green);
    //         counter++;
    //     }
    // }
    Raylib.DrawMesh(arazi, loadMaterial, Matrix4x4.Identity);

    Raylib.EndMode3D();

    Raylib.EndDrawing();
    Raylib.DrawFPS(50, 50);
}
Raylib.CloseWindow();