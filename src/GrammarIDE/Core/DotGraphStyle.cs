namespace GrammarIDE.Core
{
    public class DotGraphStyle
    {
        private string header = 
            @"digraph {{ordering=out;ranksep=.4;bgcolor=lightgrey; 
           node [shape=box, fixedsize=false, fontsize={0}, fontname=""Helvetica-bold"", fontcolor=blue, width=.25, 
                 height=.25, color=black, fillcolor=white, style=""filled, solid, bold""];
	       edge [arrowsize=.5, color=black, style=""bold""]";

        public DotGraphStyle()
        {
            FontSize = 10;
        }

        public decimal FontSize { get; set; }
        
        public string Apply(string dotgraph)
        {
            header = string.Format(header, FontSize);

            var i = dotgraph.IndexOf("n0");

            var part = dotgraph.Substring(i);

            return string.Concat(header, part);
        }
    }
}