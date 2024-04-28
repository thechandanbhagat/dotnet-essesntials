using System.Dynamic;
using System.Xml.Linq;
using System.Xml.XPath;

public static class XElementExtensions
{
	public static dynamic ToDynamic(this XElement element, List<string> columns = null)
	{
		var obj = new ExpandoObject() as IDictionary<string, object>;

		List<XElement> elementsList = new List<XElement>();
		if (columns == null || columns.Count == 0)
		{
			elementsList = element.Elements().ToList();
		}
		else
		{
			foreach (var column in columns)
			{
				var cols = column.Trim().ToLower();
				var xdocElement = new XDocument(element);
				foreach (var elem in xdocElement.Elements())
				{
					if (column.Contains(".") || column.Contains("/"))
					{
						var colArr = column.Split(".");
						var lastCol = colArr.Last().Trim();
						var lastCols = lastCol.Split("[");
						colArr[colArr.Length - 1] = lastCols[0];
						cols = string.Join('/', colArr);

						var exists = elem.XPathSelectElement("//" + cols);
						var d = elem.XPathEvaluate("//" + cols);
						if (exists != null)
						{
							var attr = lastCols.Last().Replace("]", "");
							if (exists.HasAttributes && attr != null && attr.Count() > 0)
							{
								exists.Value = exists.Attribute(attr) == null ? "" : exists.Attribute(attr).Value;
							}
							elementsList.Add(exists);
						}
						break;
					}
					else
					{
						if (elem.Name.ToString().ToLower().Equals(cols))
						{
							elementsList.Add(elem);
							break;
						}
					}
				}
			}
		}

		foreach (var el in elementsList)
		{
			obj[el.Name.ToString()] = Convert.ChangeType(el.Value, typeof(object));
		}

		return obj;
	}

	public static List<dynamic> ToDynamicList(this IEnumerable<XElement> elements)
	{
		return elements.Select(el => el.ToDynamic()).ToList();
	}

	public static bool AttributeExists(this XElement element, string attribute)
	{
		return !string.IsNullOrEmpty((string)element.Attribute(attribute));
	}

	public static bool AttributeExistsAndStartsWith(this XElement element, string attribute, string value)
	{
		return element.AttributeExists(attribute) && ((string)element.Attribute(attribute)).StartsWith(value);
	}
}