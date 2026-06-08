using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "🚗 API de Controle do Carrinho",
        Version = "v1.0",
        Description = @"
API para controle de carrinho com ESP32 via Wi-Fi.

### 🎮 Comandos disponíveis:
- **f** → Frente
- **t** → Trás
- **e** → Esquerda
- **d** → Direita
- **p** → Parar

### ⚙️ Funcionamento:
1. Cliente (web/teclado) envia comando
2. API armazena o último comando
3. ESP32 consulta e executa

Projeto educacional com controle em tempo real.
"
    });
});

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Estado
var estado = new Estado();

// GET comando
app.MapGet("/comando", () =>
{
    return Results.Json(new
    {
        comando = estado.UltimoComando,
        velocidade = estado.Velocidade
    });
})
.WithName("ObterComando")
.WithTags("Controle")
.WithSummary("Obtém o estado atual do carrinho")
.WithDescription("Retorna o comando e a velocidade atuais");

// Página web
app.MapGet("/", async context =>
{
    string html = @"
<!DOCTYPE html>
<html lang='pt-br'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Controle do Carrinho</title>
    <style>
        body {
            text-align: center;
            font-family: Arial, sans-serif;
            background: #f5f7fb;
            margin: 0;
            padding: 24px;
        }

        .card {
            max-width: 520px;
            margin: 0 auto;
            background: white;
            border-radius: 18px;
            padding: 24px;
            box-shadow: 0 8px 30px rgba(0,0,0,0.08);
        }

        h1 {
            margin-top: 0;
        }

        .grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 10px;
            max-width: 320px;
            margin: 20px auto;
        }

        button {
            width: 100%;
            height: 64px;
            border: 0;
            border-radius: 14px;
            font-size: 18px;
            cursor: pointer;
            background: #e9eef7;
            transition: 0.15s;
        }

        button:hover {
            transform: translateY(-1px);
            background: #dde6f4;
        }

        button.active {
            background: #2d6cdf;
            color: white;
            font-weight: bold;
        }

        .info {
            margin-top: 18px;
            font-size: 18px;
        }

        .status {
            display: inline-block;
            margin-top: 10px;
            padding: 10px 16px;
            border-radius: 999px;
            background: #eef3ff;
            font-weight: bold;
        }

        .hint {
            color: #555;
            font-size: 14px;
            margin-top: 12px;
            line-height: 1.5;
        }

        .stop {
            background: #ffdfdf;
        }

        .stop.active {
            background: #d92d20;
            color: white;
        }
    </style>
</head>
<body>
    <div class='card'>
        <h1>🚗 Controle do Carrinho</h1>
        <p>Use as setas do teclado ou os botões da tela</p>

        <div class='grid'>
            <div></div>
            <button id='btn-f' onclick='enviar(""f"")'>⬆ Frente</button>
            <div></div>

            <button id='btn-e' onclick='enviar(""e"")'>⬅ Esquerda</button>
            <button id='btn-p' class='stop' onclick='enviar(""p"")'>⏹ Parar</button>
            <button id='btn-d' onclick='enviar(""d"")'>➡ Direita</button>

            <div></div>
            <button id='btn-t' onclick='enviar(""t"")'>⬇ Trás</button>
            <div></div>
        </div>

        <div class='info'>
    <div>Status atual:</div>
    <div class='status' id='status'>parado</div>

    <br><br>

    <label>
        Velocidade:
        <span id='valorVel'>180</span>
    </label>

    <br><br>

    <input
        type='range'
        id='velocidade'
        min='0'
        max='255'
        value='180'
        style='width:250px;'>
    </div>

        

        <div class='hint'>
            Setas: ↑ frente, ↓ trás, ← esquerda, → direita.<br>
            Soltar a tecla envia <b>parar</b>.
        </div>
    </div>

    <script>
        let comandoAtual = 'p';

        function nomeComando(comando) {
            if (comando === 'f') return 'frente';
            if (comando === 'e') return 'esquerda';
            if (comando === 'd') return 'direita';
            if (comando === 't') return 'trás';
            return 'parado';
        }

        function limparAtivos() {
            document.querySelectorAll('button').forEach(b => b.classList.remove('active'));
        }

        function marcarAtivo(comando) {
            limparAtivos();

            const mapa = {
                'f': 'btn-f',
                'e': 'btn-e',
                'd': 'btn-d',
                't': 'btn-t',
                'p': 'btn-p'
            };

            const btn = document.getElementById(mapa[comando]);
            if (btn) btn.classList.add('active');

            document.getElementById('status').innerText = nomeComando(comando);
        }

        async function enviar(comando) {
            comandoAtual = comando;
            marcarAtivo(comando);

            await fetch('/comando', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ comando: comando })
            });
        }

        document.addEventListener('keydown', async function (event) {
            if (event.repeat) return;

            if (event.key === 'ArrowUp') {
                event.preventDefault();
                await enviar('f');
            }
            else if (event.key === 'ArrowDown') {
                event.preventDefault();
                await enviar('t');
            }
            else if (event.key === 'ArrowLeft') {
                event.preventDefault();
                await enviar('e');
            }
            else if (event.key === 'ArrowRight') {
                event.preventDefault();
                await enviar('d');
            }
            else if (event.key === ' ') {
                event.preventDefault();
                await enviar('p');
            }
        });

        document.addEventListener('keyup', async function (event) {
            if (
                event.key === 'ArrowUp' ||
                event.key === 'ArrowDown' ||
                event.key === 'ArrowLeft' ||
                event.key === 'ArrowRight'
            ) {
                event.preventDefault();
                await enviar('p');
            }
        });
        document.getElementById('velocidade')
.addEventListener('input', async function () {

    document.getElementById('valorVel')
        .innerText = this.value;

    await fetch('/velocidade', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            velocidade: parseInt(this.value)
        })
    });
});
        marcarAtivo('p');
    </script>
</body>
</html>";

    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(html);
});

// POST comando
app.MapPost("/comando", (ComandoRequest data) =>
{
    if (data != null)
    {
        estado.UltimoComando = data.Comando;
        Console.WriteLine($"Comando recebido: {estado.UltimoComando}");
    }

    return Results.Ok(new { status = "Comando recebido", comando = estado.UltimoComando });
})
.WithName("EnviarComando")
.WithTags("Controle")
.WithSummary("Envia comando para o carrinho")
.WithDescription("Define o próximo movimento do carrinho");

app.MapPost("/velocidade", (VelocidadeRequest data) =>
{
    if (data != null)
    {
        estado.Velocidade = Math.Clamp(data.Velocidade, 0, 255);

        Console.WriteLine($"Velocidade alterada para: {estado.Velocidade}");
    }

    return Results.Ok(new
    {
        status = "Velocidade atualizada",
        velocidade = estado.Velocidade
    });
})
.WithName("AlterarVelocidade")
.WithTags("Controle")
.WithSummary("Altera a velocidade do carrinho")
.WithDescription("Define uma velocidade entre 0 e 255");



app.Run();

// Classes
class Estado
{
    public string UltimoComando { get; set; } = "p";

    public int Velocidade { get; set; } = 180;
}

public class ComandoRequest
{
    /// <summary>
    /// Comando do carrinho
    /// </summary>
    /// <example>f</example>
    public string Comando { get; set; } = "";
}
public class VelocidadeRequest
{
    public int Velocidade { get; set; }
}