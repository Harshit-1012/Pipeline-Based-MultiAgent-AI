using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Models;
using AiMultiAgentUI.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AiMultiAgentUI.Engines
{
    public class SemanticKernelWorkflowEngine : IWorkflowEngine
    {
        private readonly AgentLogger _logger;
        private readonly IChatCompletionService _chat;

        public SemanticKernelWorkflowEngine(
            IConfiguration config,
            AgentLogger logger)
        {
            _logger = logger;
#pragma warning disable SKEXP0070
            var builder = Kernel.CreateBuilder();
            //builder.AddGoogleAIGeminiChatCompletion(modelId: config["Gemini:Model"], apiKey: config["Gemini:ApiKey"]);
#pragma warning restore SKEXP0070
            builder.AddOpenAIChatCompletion(
    modelId: "deepseek/deepseek-chat",
    //apiKey: config["OpenRouter:ApiKey"],
    apiKey: "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",
    endpoint: new Uri("https://openrouter.ai/api/v1"));



            //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
            var kernel = builder.Build();
            _chat = kernel.GetRequiredService<IChatCompletionService>();
        }

        public async Task<WorkflowResult> RunAsync(string idea)
        {
            await _logger.Log("🚀 SK Engine started");
            var requirements = await Ask($"""
                You are an experienced Product Owner.
                

Project Idea:
{idea}

Analyze the idea and create a software requirements document.

Include:

1. Project Summary
2. Business Goals
3. Target Users
4. Functional Requirements
5. Non-Functional Requirements
6. User Stories
7. Success Criteria
8. Assumptions and Constraints

Provide the output in clear markdown format.
""");

            var architecture = await Ask($"""
You are a Senior Software Architect.

Requirements:
{requirements}

Design a complete technical architecture.

Include:

1. Architecture Overview
2. Recommended Tech Stack
3. Frontend Components
4. Backend Components
5. Database Design
6. External Integrations
7. Security Considerations
8. Scalability Considerations
9. API Design Overview
10. Deployment Architecture

Provide diagrams in text format where useful.

Output in markdown.
""");

            var code = await Ask($"""
You are a Senior Full Stack Developer.

Requirements:
{requirements}

Architecture:
{architecture}

Create an implementation plan.

Include:

1. Project Structure
2. API Endpoints
3. Database Schema
4. Core Classes
5. Sample Backend Code
6. Error Handling Strategy
7. Validation Strategy
8. Development Roadmap

Generate realistic code snippets where appropriate.

Output in markdown.
""");

            var review = await Ask($"""
You are a Principal Engineer performing a design and code review.

Requirements:
{requirements}

Architecture:
{architecture}

Implementation:
{code}

Review the solution.

Include:

1. Strengths
2. Risks
3. Security Concerns
4. Scalability Concerns
5. Performance Concerns
6. Missing Requirements
7. Code Quality Assessment
8. Recommended Improvements
9. Final Verdict

Provide actionable feedback.

Output in markdown.
""");
            return new WorkflowResult
            {
                Requirements = requirements,
                Architecture = architecture,
                Code = code,
                Review = review
            };
        }

        private async Task<string> Ask(string prompt)
        {
            var res = await _chat.GetChatMessageContentAsync(prompt);
             return res.Content ?? "";
        }
    }
}
