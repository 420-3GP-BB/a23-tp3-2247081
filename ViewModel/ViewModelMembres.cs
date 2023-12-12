using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Xml;
using Model;

namespace ViewModel
{
    public class ViewModelMembres : INotifyPropertyChanged
    {
        //Variable pour le PropertyCHangedEventHandler
        public event PropertyChangedEventHandler? PropertyChanged;

        //Variable des Model
        private ModelMembre _modelmembre;
        private Membres _membres;
        private ModelLivre _modellivre;
        private string? _nomFichier;

        // Variable contennat le MembresActive avec leurs livre et les commandes (dernierUtilisateur)
        public Membres? MembresActive 
        {
            get;
            set;
        }

        // Variable qui prend le nom du MembresActive
        public string LastActive
        {
            get;
            set;
        }

        // Liste contenant les livres du MembresActive
        public ObservableCollection<Livres>? LivresUtilisateur
        {
            get => MembresActive.membreLivres;
        }

        // Liste contenant les commandes en attente  de l'utilisateur
        public ObservableCollection<Livres>? CommandesUtilisateurAttente
        {
            get => MembresActive.membreCommandeAttente;
        }

        //Liste contenant les commandes en attente pour la fenêtre ModeAdmin
        public ObservableCollection<string>? CommandesUtilisateurAttenteAdmin
        {
            get;
            set;
        }

        //Liste contenant les commandes traitées de l'utilisateur
        public ObservableCollection<Livres>? CommandesUtilisateurTraiter
        {
            get => MembresActive.membreCommandeTraiter;
        }

        //Liste contenant les commandes traitées pour la fenêtre ModeAdmin
        public ObservableCollection<string>? CommandesUtilisateurTraiterAdmin
        {
            get;
            set;
        }

        //Liste contenant les membres dans le XML
        public ObservableCollection<Membres>? ListeMembres
        {
            get
            {
                return _modelmembre.listeMembres; //Prends les données dans le modelMembre
            }
        }

        //Liste contenant seulement les membres en string
        public ObservableCollection<string>? ListeMembresOnly
        {
            get
            {
                return _modelmembre.listeMembresOnly; //Prends les données dans le modelMembre
            }
        }

        //Constructeur (Initialiser les variables)
        public ViewModelMembres()
        {
            _membres = new Membres();
            _modellivre = new ModelLivre();
            MembresActive = null;
            _nomFichier = null;
        }

        //Méthode permettant de charger les utilisateur avec leurs informations (nom et administrateur)
        public void ChargerMembres(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modelmembre = new ModelMembre(_modellivre.livresDictionary); //Initialise livresDictionnaire
            _modelmembre.ChargerFichierXml(_nomFichier); //Charger les informations des membres
            if (ListeMembres != null && ListeMembres.Count > 0) //Set temporairement le dernierUtilisateur
            {
                MembresActive = ListeMembres[0];
            }
            ChargerLastUser(nomFichier); //Méthode permettant de charger le dernier utilisateur
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }
        //Méthode permettant de charger les utilisateur en stirng seulement pour le comBox
        public void ChargerMembresOnly(string nomFichier)
        {
            _modelmembre = new ModelMembre(_modellivre.livresDictionary); //Initialise livresDictionnaire
            _nomFichier = nomFichier;
            _modelmembre.ChargerAllUser(_nomFichier); //Méthode permettant de charger les utilisateurs
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }

        //Méthode permettant de charger le dernierUtilisateur
        public void ChargerLastUser(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modelmembre.ChargerLastUser(_nomFichier); //Méthode dans modelMembre qui charge le dernierUtilisateur
            LastActive = _modelmembre._dernierUtilisateur; //Set le nom à LastActive pour faire l'affichage avec binding
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }

        //Méthode permettant de changer les utilisateurs et sauvegarder le nouveau dernierUtilisateur
        public void ChangerMembre(string nomFichier)
        {
            XmlDocument doc = new XmlDocument(); //Initialiser le XmlDocument
            doc.Load(nomFichier);

            XmlElement rootElement = doc.DocumentElement;
            rootElement.SetAttribute("dernierUtilisateur", LastActive); //Set le nouveau dernierUtilisateur
            doc.Save(nomFichier); //Sauvegarder le nouveau dernierUtilisateur

            ChargerUserLivre(nomFichier); //Charger leur livre
            ChargerMembres(nomFichier); //Recharger les membres
            ChargerLastUser(nomFichier); //Recharger les dernierUtilisateur pour l'affichage
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }

        //Méthode permettant de charger les livres de l'utilisateur
        public void ChargerUserLivre(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modellivre.ChargerLivres(_nomFichier); //Méthode venant de modelLivre qui charge le dictionnaire et listeLivre
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }

        //Méthode permettant de transferer les livres d'un utilisateur à un autre
        public void TransferLivre(string nomFichier, string selectedItem, string nomTransfer)
        {
            bool checkBreak = false; //Variable nécessaire pour la sauvegarde en bas
            foreach (Livres livre in LivresUtilisateur) //Livre de l'utilisateur actuelle
            {
                if (livre.ToString() == selectedItem) //Condition qui regarde si c'est le livre selectionné
                {
                    LivresUtilisateur.Remove(livre); //Si oui, on l'enlève
                    break; //Break nécessaire en raison du modification (sort du foreach)
                }
            }
            
            //Split pour prendre seulement le titre du selectedItem
            string[] subs = selectedItem.Split(',');

            foreach (var sub in selectedItem)
            {
                selectedItem = subs[0]; //Reset la variable selectedItem pour contenir seulement le titre du livre
                break;
            }

            XmlDocument doc = new XmlDocument(); //Initialisation du XmlDocment
            doc.Load(nomFichier);
            XmlElement rootElement = doc.DocumentElement;

            XmlElement membresElement = rootElement["membres"];
            XmlNodeList lesMembresXML = membresElement.GetElementsByTagName("membre");

            foreach (XmlElement elementMembre in lesMembresXML) //Liste des membres
            {
                string nom = elementMembre.GetAttribute("nom"); //Prendre leur nom
                if (MembresActive._Nom == nom) //Cherche l'utilisateur présent
                {
                    XmlNodeList livreList = elementMembre.GetElementsByTagName("livre");
                    foreach (XmlElement elementLivre in livreList)
                    {
                        string ISBN13 = elementLivre.GetAttribute("ISBN-13"); //Prendre leurs ISBN13

                        foreach (Livres livre in _modellivre.listeLivres) //Liste des livres
                        {
                            if (livre._Titre == selectedItem && livre._ISBN13 == ISBN13) //Condition qui regarde si le titre et le ISBN-13 match le livre selectionné
                            {
                                elementMembre.RemoveChild(elementLivre); //Si oui, on l'enlève
                                doc.Save(nomFichier); //Sauvergarde le fichier
                                checkBreak = true; //En raison des changements, les break sont nécessaire
                                break; //Sortir du foreach
                            }
                        }
                        if (checkBreak)
                        {
                            break; //Sortir du foreach
                        }
                    }
                }
                checkBreak = false;
                if (nomTransfer == nom) //Contidion qui regarde à qui on transfert
                {
                    XmlNodeList livresList = elementMembre.GetElementsByTagName("livre");
                    foreach (XmlElement livreNode in livresList)
                    {
                        foreach (Livres livre in _modellivre.listeLivres) //Liste de tous les livres
                        {
                            if (livre._Titre == selectedItem) //COndition qui regarde si le titre du livre match le selectedItem
                            {
                                XmlElement nouveauLivreMembre = doc.CreateElement("livre"); //Création du nouveau élément
                                nouveauLivreMembre.SetAttribute("ISBN-13", livre._ISBN13); //Set l'attribut
                                elementMembre.AppendChild(nouveauLivreMembre); //Append

                                doc.Save(nomFichier); //Sauvegarder le changement
                                checkBreak = true; //En raison des changements, les break sont nécessaire
                                break;
                            }
                        }
                        if (checkBreak)
                        {
                            break; //En raison des changements, les break sont nécessaire
                        }
                    }
                }
            }
        }

        //Méthode qui re garde si ISBN contient seulement des nombres
        public bool IsDigitsOnlyISBN(string ISBN)
        {
            foreach (char checkChar in ISBN)
            {
                if (checkChar < '0' || checkChar > '9')
                {
                    return false; //Si oui, return false
                }   
            }

            return true; //Sinon, return true
        }

        //Méthode qui regarde si l'année contient des lettres
        public bool IsDigitsOnlyAnnee(string Annee)
        {
            if (!Annee.Equals(""))
            {
                int startIndex = 0;
                if (Annee[0] == '-')
                {
                    startIndex = 1;
                }

                for (int i = startIndex; i < Annee.Length; i++)
                {
                    if (!char.IsDigit(Annee[i]))
                    {
                        return false; //Si oui, return false
                    }
                }

                return true; //Sinon, return true
            }
            else
            {
                return false; //Si oui, return false
            }
        }

        //Méthode qui ajoute une nouvelle commande
        public void newLivres(string nomFichier, string ISBN13, string Titre, string Auteur, string Editeur, string Annee)
        {
            Livres livre = new Livres(ISBN13, Titre, Auteur, Editeur, Annee); //set le nouveau livre
            if (!_modellivre.livresDictionary.ContainsKey(ISBN13) || !_modellivre.livresDictionary.ContainsValue(livre)) //Condition permettant de regarde à l'ajout d'un nouveau s'il existe déjà dans le dictionnaire
            {
                _modellivre.listeLivres.Add(livre); //Ajoute le nouveau livre dans la listeLivres
                CommandesUtilisateurAttente.Add(livre); //Ajoute le nouveau livre dans CommandesUtilisateurAttente
                _modelmembre.livresDictionnaire.Add(ISBN13, livre); //Ajoute le nouveau livre dans le dictionnaire
                _modelmembre.SauvegarderLivre(livre, nomFichier); //Sauvegarder du changement
                OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
            }
        }

        //Méthode qui enlève la commande en attente
        public void deleteCommande(string selectedItem, string nomFichier)
        {
            foreach (Livres livre in CommandesUtilisateurAttente) //Loop des commandes en attente de l'utilisateur
            {
                if (livre.ToString() == selectedItem) //Condition qui regarde si le livre courant match avec le selectedItem
                {
                    CommandesUtilisateurAttente.Remove(livre); //Enlève le livre
                    SauvegarderLivreCommande(selectedItem, nomFichier); //Sauvegarde du changement
                    break; //En raison des changements, le break est nécessaire
                }
            }
        }

        //Méthode qui sauvegarde les changements dans commande en attente
        public void SauvegarderLivreCommande(string selectedItem, string nomFichier)
        {
            bool checkBreak = false;
            //Split pour prendre seulement le titre du selectedItem
            string[] subs = selectedItem.Split(',');

            foreach (var sub in selectedItem)
            {
                selectedItem = subs[0]; //Reset la variable selectedItem pour contenir seulement le titre du livre
                break;
            }

            XmlDocument doc = new XmlDocument(); //Initialisation du XmlDocument
            doc.Load(nomFichier);
            XmlElement rootElement = doc.DocumentElement;

            XmlElement membresElement = rootElement["membres"];
            XmlNodeList lesMembresXML = membresElement.GetElementsByTagName("membre");

            foreach (XmlElement elementMembre in lesMembresXML) //Loop de tous les membres
            {
                string nom = elementMembre.GetAttribute("nom"); //Set la variable nom pour la condition en bas
                if (_modelmembre._dernierUtilisateur == nom) //Condition qui regarde dans quel utilisateur le changement a été effectué
                {
                    XmlNodeList commandesList = elementMembre.GetElementsByTagName("commande");
                    foreach (XmlElement commandeNode in commandesList)
                    {
                        string ISBN13 = commandeNode.GetAttribute("ISBN-13"); //Set la variable ISBN-13 pour la condition en bas

                        foreach (Livres livre in _modellivre.listeLivres)
                        {
                            if (livre._Titre == selectedItem && ISBN13 == livre._ISBN13) //Condition qui regarde si le titre et ISBN match avec le livre à enlever
                            {
                                elementMembre.RemoveChild(commandeNode); //Enlève l'élément
                                doc.Save(nomFichier); //Sauvergarder les changements
                                checkBreak = true;
                                break; //En raison des changements, le break est nécessaire
                            }
                        }
                        if (checkBreak)
                        {
                            break; //En raison des changements, le break est nécessaire
                        }
                    }
                    if (checkBreak)
                    {
                        break; //En raison des changements, le break est nécessaire
                    }
                }
            }
        }

        //Méthode qui charge les livres et les commandes de l'utilisateur pour le mode administrateur
        public void ChargerMembresLivres(string nomFichier)
        {
            CommandesUtilisateurAttenteAdmin = new ObservableCollection<string>(); //Initialiser la liste
            CommandesUtilisateurTraiterAdmin = new ObservableCollection<string>(); //Initialiser la liste
            XmlDocument doc = new XmlDocument(); //Initialiser le XmlDocument
            doc.Load(nomFichier);
            XmlElement rootElement = doc.DocumentElement;

            XmlElement membresElement = rootElement["membres"];
            XmlNodeList lesMembresXML = membresElement.GetElementsByTagName("membre");

            foreach (XmlElement elementMembre in lesMembresXML) //Loop de tous les membres
            {
                string nom = elementMembre.GetAttribute("nom"); //Set le nom courant
                XmlNodeList commandesList = elementMembre.GetElementsByTagName("commande"); //Loop de tous les commandes
                foreach (XmlElement commandeNode in commandesList)
                {
                    string ISBN13 = commandeNode.GetAttribute("ISBN-13");
                    string statut = commandeNode.GetAttribute("statut");

                    if (_modelmembre.livresDictionnaire.ContainsKey(ISBN13)) //Condition qui regarde si le livre existe dans le dictionnaire
                    {
                        if (statut.Equals("Attente")) //Séparation des commandes selon leur statut
                        {
                            CommandesUtilisateurAttenteAdmin.Add($"{_modelmembre.livresDictionnaire[ISBN13]} ==> {nom}"); //Ajout de la commande si son statut est "Attente"
                        }
                        else if (statut.Equals("Traitee")) //Séparation des commandes selon leur statut
                        {
                            CommandesUtilisateurTraiterAdmin.Add($"{_modelmembre.livresDictionnaire[ISBN13]} ==> {nom}"); //Ajout de la commande si son statut est "Traitee"
                        }
                    }
                }
            }
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }

        //Méthode qui change la commande en attente à la commande traitée
        public void ChangerAttentetoTraitee(string selectedItem, string nomFichier)
        {
            //Split pour prendre seulement de titre
            string selectedItemSplit = "";
            string[] subs = selectedItem.Split('=');

            foreach (var sub in selectedItem)
            {
                selectedItemSplit = subs[0];
                break;
            }

            foreach (Livres livre in CommandesUtilisateurAttente) //Loop pour enlever le selectedItem dans la commande en attente
            {
                if (livre.ToString() + " " == selectedItemSplit) //Condition qui regarde si le titre match
                {
                    CommandesUtilisateurAttente.Remove(livre); //Enlève dans CommandesUtilisateurAttente
                    CommandesUtilisateurTraiter.Add(livre); //Ajout dans CommandesUtilisateurTraiter
                    break;
                }
            }
            CommandesUtilisateurAttenteAdmin.Remove(selectedItem); //Enlève dans CommandesUtilisateurAttente pour admin
            CommandesUtilisateurTraiterAdmin.Add(selectedItem); //Ajout dans CommandesUtilisateurTraiter pour admin
            SauvegarderAttentetoTraitee(selectedItem, nomFichier); //Sauvegarde du changement
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }

        //Méthode qui sauvegarde les changements entre commande en attente et commande traitée
        public void SauvegarderAttentetoTraitee(string selectedItem, string nomFichier)
        {
            string[] subs = selectedItem.Split(',');

            foreach (var sub in selectedItem)
            {
                selectedItem = subs[0];
                break;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);
            XmlElement rootElement = doc.DocumentElement;

            XmlElement membresElement = rootElement["membres"];
            XmlNodeList lesMembresXML = membresElement.GetElementsByTagName("membre");

            foreach (XmlElement elementMembre in lesMembresXML)
            {
                //string nom = elementMembre.GetAttribute("nom");
                XmlNodeList commandesList = elementMembre.GetElementsByTagName("commande");
                foreach (XmlElement commandeNode in commandesList)
                {
                    string ISBN13 = commandeNode.GetAttribute("ISBN-13");

                    foreach (Livres livre in _modellivre.listeLivres) //Loop de tous les livres
                    {
                        if (livre._Titre == selectedItem && ISBN13 == livre._ISBN13) //Condition qui regarde si le titre et ISBN13 match à la commande
                        {
                            commandeNode.SetAttribute("statut", "Traitee");
                            doc.Save(nomFichier); //Sauvegarder les changements
                            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
                        }
                    }
                }
            }
        }

        //Méthode qui effectue les changements entre les commandes traitées et les livres de l'utilisateur
        public void ChangerTraiteetoLivre(string selectedItem, string nomFichier)
        {
            string nomUser = null;
            //Split pour prendre seulement le titre
            string[] subs = selectedItem.Split('=', '>');
            foreach (var sub in selectedItem)
            {
                selectedItem = subs[0];
                nomUser = subs[3];
                break;
            }

            foreach (Livres livre in _modellivre.listeLivres) //Loop de tous les livres
            {
                if (livre.ToString() + " " == selectedItem) //Condition qui regarde si le livre match avec le selectedItem
                {
                    CommandesUtilisateurTraiter.Remove(livre); //Enlève la commande traitée aux livres de l'utilisateur
                    CommandesUtilisateurTraiterAdmin.Remove(selectedItem + "==>" + nomUser); //Enlève la commande traitée aux livres de l'utilisateur pour admin
                    LivresUtilisateur.Add(livre); //Ajoute dans les livres de l'utilisateur présent
                    break;
                }
            }
            SauvegarderTraiteetoLivre(selectedItem, nomFichier, nomUser); //Appelle de la méthode de sauvegarde
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }

        //Méthode qui sauvegarde les changements de la méthode ChangerTraiteetoLivre
        public void SauvegarderTraiteetoLivre(string selectedItem, string nomFichier, string nomUser)
        {
            bool checkBreak = false;

            //Prends seulement le titre
            string[] subs = selectedItem.Split(',');

            foreach (var sub in selectedItem)
            {
                selectedItem = subs[0];
                break;
            }

            XmlDocument doc = new XmlDocument(); //Initialise le XmlDocument
            doc.Load(nomFichier);
            XmlElement rootElement = doc.DocumentElement;

            XmlElement membresElement = rootElement["membres"];
            XmlNodeList lesMembresXML = membresElement.GetElementsByTagName("membre");

            foreach (XmlElement elementMembre in lesMembresXML) //Loop de tous les membres
            {
                string nom = elementMembre.GetAttribute("nom"); //Set le nom courant du loop
                foreach (Livres livre in _modellivre.listeLivres) //Loop sur tous les livres
                {
                    if (livre._Titre == selectedItem && " " + nom == nomUser) //Condition qui regarde si le titre et le nom courant match
                    {
                        XmlElement nouveauLivreMembre = doc.CreateElement("livre"); //Si oui, creation du nouveau élément livre
                        nouveauLivreMembre.SetAttribute("ISBN-13", livre._ISBN13); //Set ses attributs
                        elementMembre.AppendChild(nouveauLivreMembre); //Append


                        doc.Save(nomFichier); //Sauvegarder les changements dans le fichier
                    }
                }
                XmlNodeList lesCommandesXML = elementMembre.GetElementsByTagName("commande"); //Loop de tous les commandes de l'utilisateur
                foreach (XmlElement elementCommande in lesCommandesXML)
                {
                    string ISBN13 = elementCommande.GetAttribute("ISBN-13"); //Set ISBN13
                    foreach (Livres livre in _modellivre.listeLivres)
                    {
                        if (livre._Titre == selectedItem && livre._ISBN13 == ISBN13 && " " + nom == nomUser) //Condition qui regarde le titre, le nom et l'ISBN13 s'il match
                        {
                            elementMembre.RemoveChild(elementCommande); //Si oui, on enlève
                            doc.Save(nomFichier); //Sauvegarder le changement
                            checkBreak = true;
                            break; //En raison des changements, le break est nécessaire
                        }
                    }
                    if (checkBreak)
                    {
                        break; //En raison des changements, le break est nécessaire
                    }
                }
                if (checkBreak)
                {
                    break; //En raison des changements, le break est nécessaire
                }
            }
            OnPropertyChange(""); //Déclenche un événement de modification pour notifier les observateurs
        }

        //Méthode qui déclenche un événement de modification pour notifier les observateurs
        private void OnPropertyChange(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
