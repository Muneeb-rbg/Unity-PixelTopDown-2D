/*
/--------------------------------------------------\
| Die CraftingData-Klasse verwaltet eine Sammlung   |
| von Crafting-Rezepten, die im Spiel verwendet      |
| werden.                                           |
|                                                   |
| Funktionen der CraftingData:                      |
|                                                   |
| 1. Definiert die `CraftData`-Klasse, die den      |
|    Namen des herzustellenden Items, die benötigten |
|    Zutaten und das hergestellte Item speichert.   |
| 2. Bietet ein Array von CraftData-Objekten, die   |
|    im Inspector angezeigt und bearbeitet werden    |
|    können.                                        |
| 3. Ermöglicht den Zugriff auf die Informationen    |
|    jedes Rezepts, einschließlich der Zutaten und   |
|    des hergestellten Items.                        |
\--------------------------------------------------/
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hauptklasse für die Crafting-Daten
public class CraftingData : MonoBehaviour {
    // Array von CraftData-Objekten, die die verschiedenen Crafting-Rezepte speichern
    public CraftData[] craftingData = new CraftData[3];
}

// Repräsentiert die Daten für ein Crafting-Rezept
[System.Serializable]
public class CraftData {
    public string itemName = ""; // Name des herzustellenden Items
    public Ingredients[] ingredient = new Ingredients[2]; // Zutaten für das Rezept
    public Ingredients gotItem; // Das Item, das durch das Crafting erhalten wird
}

// Repräsentiert eine Zutat für ein Crafting-Rezept
[System.Serializable]
public class Ingredients {
    public int itemId = 0; // ID des Items (0 als Standardwert, wenn kein Item gesetzt ist)
    public ItType itemType = ItType.Usable; // Typ des Items (Standard: Usable)
    public int quantity = 1; // Menge der Zutat, die benötigt wird
}
