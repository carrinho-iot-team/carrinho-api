# 🚗 Carrinho IoT — Controle via API + ESP32

Projeto educacional para controle de um carrinho utilizando **ESP32** e uma **API web**, com comunicação via Wi-Fi em tempo real.

---

## 🧠 Como funciona

1. Um cliente (interface web ou teclado) envia um comando
2. A API armazena o último comando
3. O ESP32 consulta a API
4. O carrinho executa o movimento

---

## 🎮 Comandos disponíveis

| Comando | Ação     |
| ------- | -------- |
| `f`     | Frente   |
| `t`     | Trás     |
| `e`     | Esquerda |
| `d`     | Direita  |
| `p`     | Parar    |

---

## 📂 Estrutura do projeto

```
carrinho-iot/
├── api/        # Backend (ASP.NET Web API)
├── esp32/      # Código do carrinho (ESP32)
├── docs/       # Documentação
```

---

## 🚀 Como rodar a API

```bash
cd api/NomeDaSuaApi
dotnet run
```

Depois acesse no navegador:

```
http://localhost:5143/swagger
```

---

## 📡 ESP32

O ESP32 deve:

* Conectar ao Wi-Fi
* Fazer requisições GET para:

```
/comando
```

* Executar o movimento com base na resposta

---

## 🧑‍🤝‍🧑 Como o time deve trabalhar

### 🌿 Uso de branches

❌ Não trabalhar direto na `main`

Sempre criar uma branch:

```bash
git checkout -b feature/nome-da-tarefa
```

---

### 🔄 Fluxo de trabalho

1. Criar uma branch
2. Fazer alterações
3. Commit
4. Push
5. Abrir Pull Request
6. Revisão
7. Merge na `main`

---

## 📌 Padrão de commits

Para manter organização, usamos:

* `feat:` nova funcionalidade
* `fix:` correção de bug
* `chore:` organização/manutenção
* `docs:` documentação
* `refactor:` melhoria de código

### Exemplos:

```
feat: cria endpoint /comando
fix: corrige erro no ESP32
chore: organiza estrutura do projeto
```

---

## 📋 Tarefas do projeto

As tarefas serão organizadas em:

* **Issues**
* **Projects (Kanban)**

---

## 🎯 Objetivo

Desenvolver um sistema completo de controle remoto com:

* Backend funcional
* Comunicação em tempo real
* Integração com hardware (ESP32)
* Trabalho em equipe organizado

---

## 👨‍💻 Equipe

Projeto desenvolvido em grupo para fins educacionais.

---
## 🚀 Guia rápido para o time (Git + fluxo de trabalho)

Siga este passo a passo sempre que for trabalhar no projeto.

---

## 🧩 1. Clonar o repositório

```bash
git clone https://github.com/carrinho-iot-team/carrinho-iot.git
cd carrinho-iot
```

---

## 🔄 2. Atualizar o projeto

Antes de começar qualquer tarefa:

```bash
git pull
```

---

## 🌿 3. Criar uma branch (OBRIGATÓRIO)

Nunca trabalhe direto na `main`.

```bash
git checkout -b feature/nome-da-tarefa
```

Exemplo:

```bash
git checkout -b feature/endpoint-comando
```

---

## 💻 4. Fazer as alterações

Implemente normalmente o que foi pedido na Issue.

---

## 💾 5. Salvar alterações

```bash
git add .
git commit -m "feat: descreva o que você fez"
```

Exemplo:

```bash
git commit -m "feat: cria endpoint /comando"
```

---

## ☁️ 6. Enviar para o GitHub

Na primeira vez:

```bash
git push --set-upstream origin nome-da-branch
```

Exemplo:

```bash
git push --set-upstream origin feature/endpoint-comando
```

Depois disso, pode usar só:

```bash
git push
```

---

## 🔀 7. Criar Pull Request

1. Vá no GitHub
2. Clique em **Compare & pull request**
3. No comentário, escreva:

```
Closes #NUMERO_DA_ISSUE
```

Exemplo:

```
Closes #3
```

---

## ✅ 8. Aguardar revisão

* O líder irá revisar
* Após aprovação, será feito o merge

---

## ❌ Regras importantes

* NÃO mexer na branch `main`
* SEMPRE usar branch
* SEMPRE dar `git pull` antes de começar
* Nomear branch corretamente (`feature/...`)

---

## 🧠 Dicas

* Cada tarefa = 1 branch
* Cada branch = 1 Pull Request
* Commits devem ser claros

---
## 🔁 9. Voltar para a branch principal (main)

Depois de terminar seu trabalho ou quando quiser trocar de tarefa:

```bash
git checkout main
```

---

## 🔄 10. Atualizar a main

```bash
git pull
```

---

## 🧹 11. (Opcional) Deletar a branch local

Depois que o Pull Request for aprovado:

```bash
git branch -d nome-da-branch
```

Exemplo:

```bash
git branch -d feature/endpoint-comando
```

---

## 🗑️ 12. (Opcional) Deletar a branch no GitHub

```bash
git push origin --delete nome-da-branch
```
