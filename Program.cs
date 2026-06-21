using AiMultiAgentUI.Agents;
using AiMultiAgentUI.Engines;
using AiMultiAgentUI.Hubs;
using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

// services
//builder.Services.AddSingleton<OpenAIService>();
builder.Services.AddSingleton<OpenRouterService>();
//builder.Services.AddSingleton<GeminiProvider>();
builder.Services.AddScoped<AgentLogger>();

// agents
builder.Services.AddScoped<ProductOwnerAgent>();
builder.Services.AddScoped<ArchitectAgent>();
builder.Services.AddScoped<DeveloperAgent>();
builder.Services.AddScoped<ReviewerAgent>();

// engines
builder.Services.AddScoped<OpenAIWorkflowEngine>();
//builder.Services.AddScoped<SemanticKernelWorkflowEngine>();


builder.Services.AddScoped<IWorkflowEngine>(sp =>
{
    //var mode = builder.Configuration["EngineMode"] ?? "openai";

    //if (mode == "sk")
    //    return sp.GetRequiredService<SemanticKernelWorkflowEngine>();

    return sp.GetRequiredService<OpenAIWorkflowEngine>();
});
builder.Services.AddSingleton<IAIProvider>(sp =>
{
    //var config = sp.GetRequiredService<IConfiguration>();

    //var provider = config["AIProvider"] ?? "gemini";
    //return provider switch
    //{
    //    "gemini" => sp.GetRequiredService<GeminiProvider>(),
    //    "openRouter"=> sp.GetRequiredService<GeminiProvider>(),
    //    _ => sp.GetRequiredService<GeminiProvider>()
    //};
    return sp.GetRequiredService<OpenRouterService>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//app.MapGet("/test-ai", async (OpenAIService ai) =>
//{
//    var result = await ai.Ask("Say hello in one line");
//    return result;
//});
app.MapRazorPages();
app.MapHub<AgentHub>("/agentHub");

app.Run();
