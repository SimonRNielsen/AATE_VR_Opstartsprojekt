<!-- BEGIN CONVAI UNITY SDK -->
For Convai scene setup, configuration, or diagnosis, use `Assets/Convai SDK For Unity/AIAssistantSkills/convai-unity-sdk/SKILL.md`, then only references relevant to the task.
Use Unity MCP for generic Editor operations and Convai tools for SDK scene configuration and diagnosis; inspect targets and use returned instance IDs. Repository code and documentation work can use file and shell tools without an Editor inspection.
A setup or fix request authorizes its reversible changes: preview supported mutators, apply within scope, and verify. Resolve ambiguous targets before dependent mutations. Diagnosis-only requests do not authorize repairs.
User instructions take precedence over skill guidelines, subject to host permissions. If a skill blocks requested work, identify the file and exact rule, and complete independent authorized work.
Never request or expose API keys. Keep transcript text hidden unless the user explicitly asks for it, using the skill's opt-in tools. Never save scenes, change Play Mode, or bake a NavMesh unless explicitly requested.
Convai tools are hosted by the Unity MCP Server from `com.unity.ai.assistant`. Configure the client in Project Settings > AI > Unity MCP Server. Unity CLI/Pipeline uses a separate bridge.
<!-- END CONVAI UNITY SDK -->
