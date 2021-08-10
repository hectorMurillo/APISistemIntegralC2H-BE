
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models.Generales
{
    public static class ObjectExtension
    {

        public static Boolean IsNumber(this string value)
        {
            return value == "" ? false : value.All(Char.IsDigit);
        }

        public static string ToXml(this object obj)
        {
            return ToXml(obj, string.Empty);
        }

        public static string ToXml(this object obj, string nombre)
        {
            string dateFormat = "yyyyMMdd HH:mm:ss";
            string resultado = string.Empty;

            if (obj != null)
            {
                //XDocument xml = new XDocument();
                //XElement raiz = new XElement("Contenido");
                var tipo = obj.GetType();

                nombre = System.Xml.XmlConvert.EncodeName(nombre != string.Empty ? nombre : tipo.Name);
                XElement e = new XElement(nombre);

                var lstPropiedades = tipo.GetProperties();


                if (tipo.IsPrimitive || tipo == typeof(string) || tipo == typeof(decimal) || tipo == typeof(DateTime))
                {
                    if (tipo == typeof(DateTime))
                    {
                        e.SetAttributeValue("Valor", Convert.ToDateTime(obj).ToString(dateFormat));
                    }
                    else
                    {
                        e.SetAttributeValue("Valor", obj);
                    }

                }
                else if (tipo.GetGenericArguments().Length > 0 || tipo.IsArray)
                {
                    System.Collections.IList lst = (System.Collections.IList)obj;

                    if (lst != null)
                    {
                        if (lst.Count > 0 && lst[0].GetType() == typeof(byte))
                        {
                            e.SetAttributeValue("Base64String", Convert.ToBase64String((byte[])lst));
                        }
                        else
                        {
                            foreach (var item in lst)
                            {
                                e.Add(XElement.Parse(item.ToXml()));
                            }
                        }
                    }

                }
                else
                {
                    foreach (var propiedad in lstPropiedades)
                    {
                        var typeArg = propiedad.PropertyType.GetGenericArguments();

                        if (typeArg.Length > 0 || propiedad.PropertyType.IsArray)
                        {

                            System.Collections.IList lst = (System.Collections.IList)propiedad.GetValue(obj, null);

                            XElement elemento = new XElement(System.Xml.XmlConvert.EncodeName(propiedad.Name));

                            if (lst != null)
                            {
                                if (lst.Count > 0 && lst[0].GetType() == typeof(byte))
                                {
                                    elemento.SetAttributeValue("Base64String", Convert.ToBase64String((byte[])lst));
                                }
                                else
                                {
                                    foreach (var item in lst)
                                    {
                                        elemento.Add(XElement.Parse(item.ToXml()));
                                    }
                                }
                            }

                            e.Add(elemento);
                        }
                        else if (propiedad.PropertyType.IsClass && (propiedad.PropertyType != typeof(String) & propiedad.PropertyType != typeof(decimal) & propiedad.PropertyType != typeof(DateTime)))
                        {
                            var o = propiedad.GetValue(obj, null);
                            string valor = o.ToXml();

                            XElement elemento = XElement.Parse(valor != string.Empty ? valor : new XElement(System.Xml.XmlConvert.EncodeName(propiedad.Name)).ToString());

                            e.Add(elemento);
                        }
                        else
                        {
                            if (propiedad.PropertyType == typeof(DateTime))
                            {
                                e.SetAttributeValue(propiedad.Name, Convert.ToDateTime(propiedad.GetValue(obj, null)).ToString(dateFormat));
                            }
                            else
                            {
                                e.SetAttributeValue(propiedad.Name, propiedad.GetValue(obj, null));
                            }
                        }
                    }
                }


                resultado = e.ToString();
            }

            return resultado;

        }

    }
}
