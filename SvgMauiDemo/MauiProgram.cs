using Microsoft.Extensions.Logging;
using Vapolia.Maui.Effects;
using Vapolia.Svgs;

namespace SvgMauiDemo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseEasySvg("eyJhbGciOiJSUzI1NiIsImtpZCI6InZhcG9saWFzaWciLCJ0eXAiOiJKV1QifQ.eyJodHRwczovL3NjaGVtYXMudmFwb2xpYS5ldS8yMDIwLzA1L2NsYWltcy9MaWNlbnNlc0NsYWltIjoie1wiTGljZW5zZXNcIjpbe1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiaW9zXCIsXCJBcHBJZFwiOlwiZXUudmFwb2xpYS5zdmdtYXVpZGVtb1wiLFwiTWF4QnVpbGRcIjpcIjIwMjgtMTItMzFUMDA6MDA6MDArMDA6MDBcIn0se1wiUHJvZHVjdFwiOlwieGFtc3ZnXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcImV1LnZhcG9saWEuc3ZnbWF1aWRlbW9cIixcIk1heEJ1aWxkXCI6XCIyMDI4LTEyLTMxVDAwOjAwOjAwKzAwOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z1wiLFwiT3NcIjpcIndpbmRvd3NcIixcIkFwcElkXCI6XCJldS52YXBvbGlhLnN2Z21hdWlkZW1vXCIsXCJNYXhCdWlsZFwiOlwiMjAyOC0xMi0zMVQwMDowMDowMCswMDowMFwifSx7XCJQcm9kdWN0XCI6XCJ4YW1zdmdmb3Jtc1wiLFwiT3NcIjpcImlvc1wiLFwiQXBwSWRcIjpcImV1LnZhcG9saWEuc3ZnbWF1aWRlbW9cIixcIk1heEJ1aWxkXCI6XCIyMDI4LTEyLTMxVDAwOjAwOjAwKzAwOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwiYW5kcm9pZFwiLFwiQXBwSWRcIjpcImV1LnZhcG9saWEuc3ZnbWF1aWRlbW9cIixcIk1heEJ1aWxkXCI6XCIyMDI4LTEyLTMxVDAwOjAwOjAwKzAwOjAwXCJ9LHtcIlByb2R1Y3RcIjpcInhhbXN2Z2Zvcm1zXCIsXCJPc1wiOlwid2luZG93c1wiLFwiQXBwSWRcIjpcImV1LnZhcG9saWEuc3ZnbWF1aWRlbW9cIixcIk1heEJ1aWxkXCI6XCIyMDI4LTEyLTMxVDAwOjAwOjAwKzAwOjAwXCJ9XX0iLCJuYmYiOjE2NTc2MTA3ODUsImV4cCI6MTk3MzIyOTk4NSwiaWF0IjoxNjU3NjEwNzg1LCJpc3MiOiJodHRwczovL3ZhcG9saWEuZXUvYXV0aG9yaXR5IiwiYXVkIjoiaHR0cHM6Ly92YXBvbGlhLmV1L2F1dGhvcml0eS9saWNlbnNlcyJ9.lAUpdaTmBNhtEUlmXaEGdQvfxr05ZsLUP_GSt8Wi2uGaf8cKO0i3W5Z9VASZrAQPSZcURsPcOM1-VQcIF84GsuKNXOHuqaddY1vGbV142f5GSqXy-SiPVeZJTQPjZM1WDcpBdwlWjWzce2ZhtBVb5zoWXrf_MDqDeoIoBp98ReEvuLudsQj9a-tsIwPp6UvtlSq9_bml2yUmfrG4awWuwImyyvjCVoJSHiDEY2Ah8lqvpN-M3JQkpf58tvbwHSOw9ZLAedrY_6Iy0U34M7eQSl42-OBIcI-h4S0zBhXhRiN_Oe_kp2PSZ7RphA9S3VyHEArZyrIR8ZVCKoagJ-zWhg")
            .AddButtonEffects();;

        builder.Logging.AddDebug();
        
        return builder.Build();
    }
}