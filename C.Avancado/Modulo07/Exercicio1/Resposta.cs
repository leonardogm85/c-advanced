using System.Xml.Linq;

namespace C.Avancado.Modulo07.Exercicio1
{
    /// <summary>
    /// <para>
    ///     Crie uma aplicação que imprime na tela detalhes sobre os métodos presentes em alguma
    ///     classe do assembly System.dll. O XML abaixo mostra o resultado que deve ser obtido para a
    ///     classe object:
    /// </para>
    /// <code>
    ///     <![CDATA[
    ///     <type name="System.Object">
    ///         <method>
    ///             <name>ToString</name>
    ///             <returnType>System.String</returnType>
    ///             <params />
    ///         </method>
    ///         <method>
    ///             <name>Equals</name>
    ///             <returnType>System.Boolean</returnType>
    ///             <params>
    ///                 <param name="obj">System.Object</param>
    ///             </params>
    ///         </method
    ///         <method>
    ///             <name>Equals</name>
    ///             <returnType>System.Boolean</returnType>
    ///             <params>
    ///                 <param name="objA">System.Object</param>
    ///                 <param name="objB">System.Object</param>
    ///             </params>
    ///         </method>
    ///         <method>
    ///             <name>ReferenceEquals</name>
    ///             <returnType>System.Boolean</returnType>
    ///             <params>
    ///                 <param name="objA">System.Object</param>
    ///                 <param name="objB">System.Object</param>
    ///             </params>
    ///         </method>
    ///         <method>
    ///             <name>GetHashCode</name>
    ///             <returnType>System.Int32</returnType>
    ///             <params />
    ///         </method>
    ///         <method>
    ///             <name>GetType</name>
    ///             <returnType>System.Type</returnType> 
    ///             <params /> 
    ///         </method> 
    ///     </type>
    ///     ]]>
    /// </code>
    /// <para>
    ///     Cada tag method representa um método da classe. A tag name indica o nome do método;
    ///     returnType indica o tipo de retorno; e params exibe os parâmetros(nome e tipo) do método
    ///     em questão.
    /// </para>
    /// <para>
    ///     Utilize LINQ para XML para gerar o documento, que também deve ser armazenado em um
    ///     arquivo dentro do diretório da aplicação chamado methods.xml.
    /// </para>
    /// </summary>
    public static class Resposta
    {
        public static void Executar()
        {
            Type type = typeof(object);

            XElement xml = new XElement("type", new XAttribute("name", type.FullName!),
                from method in type.GetMethods()
                select new XElement("method",
                    new XElement("name", method.Name),
                    new XElement("returnType", method.ReturnType.FullName),
                    new XElement("params",
                        from parameter in method.GetParameters()
                        select new XElement("param", new XAttribute("name", parameter.Name!), parameter.ParameterType.FullName!)
                    )
                )
            );

            xml.Save("methods.xml");
        }
    }
}
