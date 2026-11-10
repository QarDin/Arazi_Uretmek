using Raylib_cs;
using System.Numerics;

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
float[,] filedValue = new float[RowDot, ColmunDot];
int counter = 0;
Vector3[,] vectors = new Vector3[RowDot, ColmunDot];
#endregion
//  10x10 arazi 
#region araziye degere atayan dongu 
for (int i = 0; i < RowDot; i++)
{
    //System.Console.WriteLine(i);
    for (int j = 0; j < ColmunDot; j++)
    {
        int valueHolder = rnd.Next(0, 20);

        filedValue[i, j] = valueHolder;
        vectors[i, j] = new Vector3(j - 5, filedValue[i, j] - 5, i - 5);
        counter++;
    }
}
for (int i = 0; i < RowDot - 1; i++)
{
    //System.Console.WriteLine(i);
    for (int j = 0; j < ColmunDot - 1; j++)
    {
        System.Console.WriteLine($"{filedValue[i, j]},{filedValue[i + 1, j]},{filedValue[i, j + 1]},{filedValue[i + 1, j + 1]} -------- {counter}");

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
            Raylib.DrawTriangle3D(vectors[i + 1, j + 1], vectors[i + 1, j], vectors[i, j + 1], Color.DarkGreen);
            counter++;
        }
    }

    Raylib.EndMode3D();

    Raylib.EndDrawing();
}
Raylib.CloseWindow();
#endregion