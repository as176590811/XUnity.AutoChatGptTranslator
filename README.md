# XUnity.AutoChatGptTranslator

Translator that uses ChatGPT and a custom prompt to translate the game in parallel.

## Installation instructions

1. Build the assembly
2. Install [XUnity.AutoTranslate]() into your game as normal
3. Place XUnity.AutoChatGptTranslator.dll into `<GameDir>/ManagedData/Translators` (Should see the other translators)
4. Update your Config.ini in `<GameDir>/AutoTranslator`
	- Add to the bottom of the file:
	- ```
		[ChatGPT]
		APIKey=
		Model=gpt-4o
		Prompt=Translate Simplified Chinese to clear, concise English in a Wuxia tone. Keep context, meaning, and structure intact. Leave special characters (e.g., HTML) unchanged. Use Pinyin for Chinese names and terms; write everything else in English. Adjust for readability without changing intent. Maintain natural capitalization for names and titles.
		```
	- Add your own APIKey from [OpenAI](https://platform.openai.com/chat-completions) please note this is not free.
	- Change the translator section at the top of the file to use the translator:
	- ```
	  [Service]
	  Endpoint=ChatGPTTranslate
	  FallbackEndpoint=
	  ```

## Fine tuning your prompt

Please note the prompt is what actually tells ChatGPT what to translate. Some things that will help:
- Update the languages eg. Simplified Chinese to English, Japanese to English
- Ensure you add context to the prompt for the game such as 'Wuxia', 'Sengoku Jidai', 'Xanxia', 'Eroge'. 
- Make sure you tell it how to translate names whether you want literal translation or keep the original names

## Packages

The assemblies included are the Dev versions of XUnity.AutoTranslator.Feel free to replace them with the latest release if it doesnt work with your current build or do not trust the pre-built assemblies.