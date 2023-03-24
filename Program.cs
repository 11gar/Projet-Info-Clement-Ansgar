World Monde = new World(7, 15, 3);
Monde.Environnement.Infill[0] = "  XXX  ";
Monde.Environnement.Infill[1] = "  X X  ";
Monde.Environnement.Infill[2] = "  XXX  ";

Monde.Equipe1.Infill[0] = "  ___  ";
Monde.Equipe1.Infill[1] = " (° °) ";
Monde.Equipe1.Infill[2] = " /| |\\ ";

Monde.Equipe2.Infill[0] = "   O   ";
Monde.Equipe2.Infill[1] = " /(_)\\ ";
Monde.Equipe2.Infill[2] = "  / \\  ";

Monde.Equipe1.FillGrid(1, 2, 1);
Monde.Equipe2.FillGrid(2, 2, 1);
Monde.Environnement.FillGrid(1, 1, 1);
Monde.Show();