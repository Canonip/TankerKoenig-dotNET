using System.Resources;
using System.Reflection;
using System.Runtime.CompilerServices;



// Allgemeine Informationen über eine Assembly werden über die folgenden
// Attribute gesteuert. Ändern Sie diese Attributwerte, um die Informationen zu ändern,
// die einer Assembly zugeordnet sind.
[assembly: AssemblyTitle("TankerKoenig")]
[assembly: AssemblyDescription("Gas Price API for German Gas Stations")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Canonip")]
[assembly: AssemblyProduct("TankerKoenig")]
[assembly: AssemblyCopyright("Copyright © 2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("de")]

// Versionsinformationen für eine Assembly bestehen aus den folgenden vier Werten:
//
//      Hauptversion
//      Nebenversion
//      Buildnummer
//      Revision
//
// Sie können alle Werte angeben oder Standardwerte für die Build- und Revisionsnummern verwenden,
// übernehmen, indem Sie "*" eingeben:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.2.0")]
[assembly: AssemblyFileVersion("1.0.2.0")]

// Internal classes need to be visible in order to test them
[assembly: InternalsVisibleTo("TankerKoenigTest")]