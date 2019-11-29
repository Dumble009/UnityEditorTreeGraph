using System.Collections.Generic;

public class CodeTemplateInterpolator
{
	static public string Interpolate(string source, CodeTemplateParameterHolder parameterHolder)
	{
		char[] charSource = source.ToCharArray();
		List<char> buffer = new List<char>();
		int sourceLength = charSource.Length;
		List<char> parameterNameBuffer = new List<char>();
		bool isParameterFound = false;
		for (int i = 0; i < sourceLength; i++)
		{
			if (!isParameterFound)
			{
				if (i <= sourceLength - 2)
				{
					if (charSource[i] == '{' && charSource[i + 1] == '{')
					{
						isParameterFound = true;
						parameterNameBuffer.Clear();
						i++;
					}
					else
					{
						buffer.Add(charSource[i]);
					}
				}
				else
				{
					buffer.Add(charSource[i]);
				}
			}
			else
			{
				if (charSource[i] == '}' && charSource[i + 1] == '}')
				{
					string parameterName = new string(parameterNameBuffer.ToArray());
					string parameterValue = parameterHolder.GetParameter(parameterName);

					char[] charParameterValue = parameterValue.ToCharArray();
					int charParameterValueLength = charParameterValue.Length;
					for (int j = 0; j < charParameterValueLength; j++)
					{
						buffer.Add(charParameterValue[j]);
					}

					isParameterFound = false;
					i++;
				}
				else
				{
					parameterNameBuffer.Add(charSource[i]);
				}
			}
		}

		string ret = new string(buffer.ToArray());

		return ret;
	}
}

public class CodeTemplateParameterHolder
{
	Dictionary<string, string> parameterDict;
	public CodeTemplateParameterHolder() {
		parameterDict = new Dictionary<string, string>();
	}

	public void SetParameter(string name, string value)
	{
		if (parameterDict.ContainsKey(name))
		{
			parameterDict[name] = value;
		}
		else
		{
			parameterDict.Add(name, value);
		}
	}

	public string GetParameter(string name)
	{
		return parameterDict[name];
	}
}
