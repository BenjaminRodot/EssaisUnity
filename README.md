# Unity2D_Bibliotheque
Création de mini-jeux/éléments génériques pour m'entrainer

## Liste des étapes :
- [Création tileset/tilemap](https://github.com/BenjaminRodot/Unity2D_Bibliotheque/edit/main/README.md#cr%C3%A9ation-tileset-tilemap-et-palette)

## Création tileset, tilemap et palette
### Tileset :
[Tuto Gimp](https://pinnguaq.com/fr/learn/pixel-art/pixel-art-3-tilling-basics-using-gimp/)
Création image Gimp en X fois définition tiles (*x fois 32px par exemple*). Créer un calque pour chaque tiles (*mettre des **bordures** facilite la vision du travail*).
> **Note**<br>
Il est possible de faire une **rotation** d'un calque pour gagner du temps. En revanche, ne pas oublier de mettre *Interpolation* en ***NoHalo*** pour éviter le flou (voir [lien](https://www.gimp-forum.net/Thread-Rotate-tool-in-2-10-blurring-layers)).

Après avoir dessiné les tiles, on peut exporter le tileset en format **PSD**.

### Tilemap :
[Tuto Unity](https://www.youtube.com/watch?v=ryISV_nH8qw)
DragAndDrop du fichier **PSD** dans Asset (ou autre dossier créé préalablement). Penser à ces quelques options à vérifier dans l'inspector :
- Sprite mode : **multiple**
- Pixels per Unit : **définition des tiles** (*32 par exemple*)
- Filter Mode : **Point** (dans le cas d'un pixel art)
- Compression : **None**

On peut maintenant ouvrir le tileset, et faire un slice automatique avec **Grid By Cell Size**.

Puis créer la tilemap dans la hiérarchie  de la scène (Clic droit -> 2D Object -> Tilemap).

### Palette
On va aller chercher la fenêtre des palettes (Window -> Tile Palette). puis créer une nouvelle palette, dans laquelle on va dragAndDrop le tileAsset qui a été créé précédemment.
