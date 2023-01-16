using System;
using System.Text;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;


namespace GedcomParser.Parsers
{
    public static class PedigreeParser
    {
        internal static Pedigree ParsePedigree(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var pedigree = new Pedigree();

            if (incomingChunk != null)
            {
                var pedigreeValue = incomingChunk.Data;
                if (!pedigreeValue.IsNullOrEmpty())
                {
                    try
                    {
                        pedigree.Type = (Pedigree.PedigreeType)Enum.Parse(typeof(Pedigree.PedigreeType), pedigreeValue, true); // case insensitive
                    }
                    catch
                    {
                        resultContainer.Errors.Add($"Failed to convert String to Enum in Pedigree Type ='{incomingChunk.Type}'");
                    }
                }
            }

            return pedigree;
        }
    }
}