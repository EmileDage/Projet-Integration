Pour ne pas se m�langer dans les ID et avoir des dupliques donner les ID selons ce syst�me :
-1 testing
0 est pour l'item vide
Les ID de 1-9 sont pour les outils exclusivement
Les ID des mat�riaux doivent avoir 2 digit
etc. pour les autres classes d'item si on en cr�e

Biomes:
	foret : 
		Pommier(pomme);
		Jade(crystal);
		Fer(mineraux);
		
	volcan :
		crocus(saffran) -> cher dans la vrai vie donc...
		Obsidian(mineraux)
		Lapis lazuli(crystal)
		
	Plage :
		Noix de Coco(Sapling de coco)
		Agate bleu(crystal)
		CoralStone(mineraux)

	Biome de kess� � Catherine :
		Diamant(crystal)
		Bismuth(mineraux)
		Champignon(spores de champigniondciosna)
	

	Comment Creer une ressource et un spawner :
		Pour les mineraux/crystaux
			clique droit : cr�er un scriptable item > materiaux
			Assigner les field, sauf Spawner et ItemWorldObject
			ATTENTION, NE PAS OUBLI� D'ASSIGNER LA FONCTION
			Maintenant, dans le fichier Yeet/base, cr�er un prefab variant de Mat_Base
			Assigner l'item, ignorer QTE
			Assinger le MeshFiler et MeshRenderer
			Ensuite, dans le fichier Spawner, dans Mineraux et crystaux box, creer une variante du minerauxplanterBase
			Assigner le scriptable que tu viens tout juste de cr�eer dans Spawned mat
			Assigner un spawn rare de choix, cette fois, c'est un itemworldobject qui est necessaire(dans le fichier yeet)
			Assigner un mesh et des materiaux dans mesh renderer et filter
			Ensuite dans le prefab, aller sur le node et assigner un mesh et des texture, le mesh et les textures devraient etre les memes que pour le yeet
			dupliquer les nodes aux besoin pour atteindre la quatit�e voulue
			d�placer les nodes et le RareSpawnTransform pour que �a ait du sense
			Ensuite, faire une variante du SpawnerMineralBase dans Mineraux et Crystaux Wild
			Faire les memes �tapes que pour l'autre spawner, sauf qu'il n'y a pas de raretransform
			Finalement retourner sur le scriptable object cr�er au d�but et assigner sous spawner le Spawner cr�er dans Mineraux et Crystaux box
			