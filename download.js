
function zippedFile(){
    var link = document.createElement("a");
    link.href = "https://github.com/NapolianMyat09/GraveyardBattlefield2.0/raw/refs/heads/main/GraveyardBattlefield2.0.zip";
    link.download = "GraveyardBattlefield2.0.zip";
    link.click();
    document.body.removeChild(link);
}
