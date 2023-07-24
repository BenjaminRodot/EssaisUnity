# Création d'assetPlayer, animation et playerMovement

## AssetPlayer

La création des Asset est le même que celui du tileset, donc ce [tuto](https://github.com/BenjaminRodot/Unity2D_Bibliotheque/blob/main/Explication/Tileset_TileMap_Palette.md#tileset-).

## Animation

[Tuto Unity](https://www.youtube.com/watch?v=hkaysu1Z-N8) DragAndDrop du fichier **PSD** dans Asset (ou autre dossier créé préalablement). Penser à ces quelques options à vérifier dans l'inspector :
- Sprite mode : **multiple**
- Pixels per Unit : **définition des tiles** (*32 par exemple*)
- Filter Mode : **Point** (dans le cas d'un pixel art)
- Compression : **None**

On peut maintenant ouvrir le **sprite editor**, et faire un slice automatique avec **Grid By Cell Size**.

Puis, on sélectionne l'objet **Player**, on ouvre la fenêtre d'animation (Window &#8594; Animation), puis ***Create animation***. On dragAndDrop les sprites intéressant pour l'animation. Ne pas oublier de modifier les **samples** si l'animation est trop lente ou trop rapide.

> **Note**<br>
Si on ne voit pas les **samples**, cliquer sur les 3 points en haut à droite de la fenêtre d'animation et ***Show samples***.

Dans le dossier *Animation* fraichement créer, double-cliquer sur *Player*. Dans l'*animator*, clic droit pour ajouter une **transition**. En haut à gauche de la fenêtre Animator, aller sur ***parameters*** et créer une variable (*Speed* par exemple) qui est utilisé comme condition des transitions.

##Player Movemement
[Tuto Unity](https://www.youtube.com/watch?v=whzomFgjT50) 
On prend les *inputs* qu'on multiplie par la vitesse de déplacement, et range dans **movement.x** et **movement.y**. Puis, on utilise MovePosition :
```cs
rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
```
> **Note**<br>
Il existe 2 solutions pour la rotation de l'animation :
- 2 animations différentes pour gauche et droite
- Un flip du sprite avec un ```Quaternion.Euler(new Vector2(0f,180f)) ``` en vérifiant l'input.

