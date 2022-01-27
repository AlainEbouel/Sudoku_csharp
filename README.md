# Rapport projet de session phase 1:

Dans la phase 1 du projet de Sudoku, nous avons eu à implementer une interface graphique d'une grille 9x9 où l'ont peut insérer des chiffres à différents endroits. Mais aussi, on a eu à établir un système qui vérifie si l'état actuel Sudoku est valide ou non, ensuite, une fonctionnalité de sauvegarde et chargement d'une grille.
Et enfin, une batterie de test unitaires automatisés qui vérifie chaque fonctionnalité.

### 1. Utilisation des principes SOLID:

- **Principe de responsabilité unique**

  ```c#
    public class BigNumber
    {
        public string Number { get; set; }

        public BigNumber()
        {
            //Number ="0";
            Number ="";
        }
    }

  ```

  - Explications et justifications:

    L'information contenue dans la classe BigNumber supporte un seul concept mais reste cohérent.Dans cette classe on utilise un string pour représenter les chiffres en gros caractères dans les cases.

- **Principe ouvert/fermé**

  ```c#
   public class BaseViewModel : INotifyPropertyChanged
  ```

  ```c#
  public class SudokuGridViewModel : BaseViewModel
  ```

  ```c#
  public class MainViewModel : BaseViewModel
  ```

  ```c#
  public class LittleSudokuGridViewModel : BaseViewModel
  ```

  ```c#
  public class LittleNumberViewModel : BaseViewModel
  ```

  ```c#
  public class IndividualCaseViewModel : BaseViewModel
  ```

  ```c#
  public class BigNumberViewModel : BaseViewModel
  ```

  - Explications et justifications:

    6 Classes dans ViewModel hérite de la Classe BaseViewModel, Chaque sous-type peut implémenter sa propre propriété qui sera notifiée à BaseViewModel qui notifie les changements. Ce qui fortifie l'extensibilité du code.

- **Principe de subtitution de Liskov**

  ```c#
   public class BaseViewModel : INotifyPropertyChanged
   {
        public string Name { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
  ```

  ```c#
  public class SudokuGridViewModel : BaseViewModel
  ```

  ```c#
  public class MainViewModel : BaseViewModel
  ```

  ```c#
  public class LittleSudokuGridViewModel : BaseViewModel
  ```

  ```c#
  public class LittleNumberViewModel : BaseViewModel
  ```

  ```c#
  public class IndividualCaseViewModel : BaseViewModel
  ```

  ```c#
  public class BigNumberViewModel : BaseViewModel
  ```

  - Explications et justifications:

    Les 6 derniers classes dans ViewModel héritent de BaseViewModel.Pour toute instance où la classe BaseViewModel est utilisée, on peut remplacer BaseViewModel par son sous-type quelconque sans risquer de briser le code.

- **Principe de ségrégation des interfaces**!

  [Lien vers les interfaces!](https://github.com/wflageol-uqtr/projet-de-session-hln-agen-far-my-jv/tree/main/sudoku/Views)

  - Explications et justifications:

    Presque tous les interfaces dans Views pouvaient être reunie en un seul. Mais, pour limiter le couplage du système et pour avoir une flexibilité optimale à l'évolution et au refactoring; facilitant ainsi la tâche pour les changements futurs.

- **Principe d'injection de dépendances**

  ```

  ```

  - Explications et justifications:

### 2. Utilisation des patrons GRASP:

- **Controleur**

  ```c#
  using Sudoku.Commands;
  using Sudoku.Services;
  using System;

  namespace Sudoku.ViewModels
  {
      class ButtonViewModel : BaseViewModel
      {
          public DelegateCommand<string> SaveCommand { get; set; }
          public DelegateCommand<string> LoadCommand { get; set; }
          public ButtonViewModel()
          {
              Name = this.GetType().Name;
              SaveCommand = new DelegateCommand<string>(Save);
              LoadCommand = new DelegateCommand<string>(Load);
          }
          private void Load(string obj)
          {
              SudokuGridViewModel.LoadGameActivate();
          }
          private void Save(string obj)
          {
              SudokuGridViewModel.SaveGameActivate();
          }
      }
  }
  ```

  - Explications et justifications:

    Le ButtonViewModel est un contrôleur puisque qu'il attribut la responsabilité de traiter les sauvegarde et les chargement de grille à une classe non-UI en passant par une commande, ce qui le fait respecter cette définission : Il est défini comme le premier objet, au-delà de la couche d'interface utilisateur, qui reçoit et coordonne (témoins), un système d'exploitation. Le contrôleur doit déléguer le travail qui doit être fait à d'autres objets; il coordonne ou contrôle l'activité. Il ne devrait pas faire beaucoup de travail lui-même.

- **Fabrication pure**
  [Les deux classes sont dans Service!](https://github.com/wflageol-uqtr/projet-de-session-hln-agen-far-my-jv/tree/main/sudoku/Services)

  - Explications et justifications:

    On a séparé quelques méthodes de VérificationClass un module nommé BackupSystem

- **Indirection**

  ```c#
    public class BigNumber
  ```

  ```c#
    public class BigNumberViewModel : BaseViewModel
  ```

  ```c#
    public partial class BigNumberView : UserControl
  ```

  - Explications et justifications:
    L'indirection est ici présente car avec le modèle BigNumber nous avons un exemple du shéma modèle-vue-contrôleur. Dans ce shéma il y a deux extrémité, le modèle(BigNumber) et le contrôleur(BigNumberView), au centre et qui sert de médiateur il y a le ViewModel(BigNumberViewModel). L'indirection est ici applicable car il n'y a pas d'interaction directe entre BigNumber et BigNumberView dans le projet, tout passe par BigNumberViewModel.

# Rapport projet de session phase 2:

Notes: Après la présentation en classe, les Tests ont été corrigés, et la prise en compte des couleurs a été ajoutée à la fonctionnalité Undo/redo.

Dans la phase 2, on a eu à apporter sur le Sudoku quelques changements. En effet, on a procédé à l'ajout de fonctionnalité undo et redo qui permet respectivement le retour sur chacun des modifications précèdentes effectuées sur la grille et l'annulation d'un ou plusieurs undo fait de suite sans modification.
On a aussi procédé à l'ajout d'un service donnant la possibilité de colorer les cases (Click droit sur la case pour changer de couleur) avec un choix de 9 couleurs énumérées dans ColorPickerService; et une dernière fonctionnalité permettant la sélection/multiselection et déselection de case (Ctrl+ Click gauche) pour y apporter des modifications.

### Utilisation de patron de conception GoF:

- **PATTERN PROXY**

```c#
   namespace Sudoku.ViewModels
   {
     public class IndividualCaseViewModel : BaseViewModel
     {
       private void Selected(string obj)
       {
          // if (ButtonViewModel.isActive)
          // {
           if (IsSelected == false)
           {
               bnvm.IsSelected = true;
               lnvm.IsSelected = true;
               IsSelected = true;
               //selected = true;
               changeBorderColor("black");
               SudokuGridViewModel.selectedCase.Add(bnvm);
               SudokuGridViewModel.selectedCase2.Add(lnvm);

               //InputSelected();
           }
           else
           {
               BorderColor = "transparent";
               bnvm.IsSelected = false;
               lnvm.IsSelected = false;
               IsSelected = false;

               IEnumerable<BigNumberViewModel> bigN = new List<BigNumberViewModel>();
               IEnumerable<LittleNumberViewModel> littleN = new List<LittleNumberViewModel>();

               bigN = SudokuGridViewModel.selectedCase.Where(a => a != bnvm);
               littleN = SudokuGridViewModel.selectedCase2.Where(a => a != lnvm);

               SudokuGridViewModel.selectedCase = new List<BigNumberViewModel>();
               SudokuGridViewModel.selectedCase2 = new List<LittleNumberViewModel>();

               foreach (var bN in bigN)
                   SudokuGridViewModel.selectedCase.Add(bN);

               foreach (var lN in littleN)
                   SudokuGridViewModel.selectedCase2.Add(lN);
           }

       }}
```

- Explications et justifications:

  La classe `IndividualCaseViewModel ` constitue une classe proxy. En effet la fonctionnalité sélection/déselection de cellules qui permet d'effectuer des changements sur toutes ces cellules(les `bigNumber` et `littleNumberModel` y compris) en même temps, est implémenter dans cette classe. Cette dernière sert d'intermédiaire entre l'utilisateur et les `bigNumber/littleNumberModel` lors des changements sur la sélection en transmettant tous les appels de méthodes aux cibles. Permettant ainsi le changement spontané des notations sur la selection complète.

  NB: CTRL+LeftClick pour selectionner. LeftClick pour changer de couleur.

- **PATTERN BRIDGE**

```c#
  public class BackupSystem:
  public static void Undo()
  public static void Redo()
  private static void Load(string savedData)

```

```c#
class ButtonViewModel : BaseViewModel{
 private void Load(string obj)
      {
          BackupSystem.StackIsLocked = true;
          BackupSystem.LoadingActivation("backupFile.txt");
          BackupSystem.StackIsLocked = false;
      }
      private void Save(string obj)
      {
          BackupSystem.BackupActivation();
      }

      private void Undo(string obj)
      {
          if (BackupSystem.UndoStack.Count > 1)
          {
              BackupSystem.Undo();
          }
      }

      private void Redo(string obj)
      {
          if (BackupSystem.RedoStack.Count > 0)
          {
              BackupSystem.Redo();
          }
      }}
```

```xaml
<UserControl x:Class="Sudoku.Views.ButtonView"
           xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
           xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
           xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
           xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
           xmlns:local="clr-namespace:Sudoku.Views"
           mc:Ignorable="d"
           d:DesignHeight="450" d:DesignWidth="800">
  <Grid>
      <Grid.ColumnDefinitions>
          <ColumnDefinition></ColumnDefinition>
          <ColumnDefinition></ColumnDefinition>
      </Grid.ColumnDefinitions>

      <Grid.RowDefinitions>
          <RowDefinition></RowDefinition>
          <RowDefinition></RowDefinition>
          <RowDefinition></RowDefinition>
      </Grid.RowDefinitions>
      <Button Grid.Column="0" Command="{Binding SaveCommand}">Save</Button>
      <Button Grid.Column="1" Command="{Binding LoadCommand}">Load</Button>
      <Button Grid.Column="0" Grid.Row="2" Command="{Binding UndoCommand}">Undo</Button>
      <Button Grid.Column="1" Grid.Row="2" Command="{Binding RedoCommand}">Redo</Button>
      <Button Content="{Binding ButtonText}" Command="{Binding MultipleCommand}" Grid.ColumnSpan="2" Grid.Row="1"></Button>

  </Grid>
</UserControl>
```

- Explications et justifications:

  On a divisé l'implémentation de la fonctionnalité undo et redo en deux parties : Une partie dans ` BackupSystem` une autre sur `ButtonViewModel`. Cette liaison abstraite permet aux variétés de buttonViewModel de conserver leur comportement souhaité. Parceque, découpler l'abstraction d'un concept de son implémentation.
  permet à l'abstraction et l'implémentation de varier indépendamment.

- **PATTERN MEMENTO**

```C#
        public static void Undo()
        {
            StackIsLocked = true;
            Undo_redo_is_Active = true;
            RedoStack.Push(UndoStack.Pop());

            Load(UndoStack.Peek());
            StackIsLocked = false;
            Undo_redo_is_Active = false;
        }

        public static void Redo()
        {
            StackIsLocked = true;
            Undo_redo_is_Active = true;
            UndoStack.Push(RedoStack.Pop());

            Load(UndoStack.Peek());
            StackIsLocked = false;
            Undo_redo_is_Active = false;
        }
```

- Explications et justifications:

  Le principe de undo et redo en soit est un memento, car il permet de conserver un état de la grillet et d'y revenir en utilisant les boutons désigné. Le booléen Undo_redo_is_Active peut être comparer à l'attribut state dans un memento. Notre exemple de undo et redo peut être comparer à un autre très bon exemple de memento, la même fonctionnalité dans les Bloc-Note. Il y est comparable puisqu'il permet de conserver un état de la grille et d'y revenir si nécessaire tout comme la fonctionnalité dans le bloc-note permet de conserver la dernière modification et d'y revenir.

| Tâche                              | Responsable        |
| ---------------------------------- | ------------------ |
| Interface graphique (phase 1)      | Jérémy             |
| Notation spéciales (phase 1)       | Jérémy             |
| Système de vérification (phase 1)  | Hervé              |
| Sauvegarde et chargement (phase 1) | Alain Gires        |
| Tests unitaires (phase 1)          | Félix-Antoine      |
| Rapport (phase 1)                  | Médoune, Jérémy    |
| Undo/Redo (phase 2)                | Alain Gires        |
| Couleurs (phase 2)                 | Jérémy             |
| Sélection multiple (phase 2)       | Hervé, Alain Gires |
| Tests unitaires (phase 2)          | Félix-Antoine      |
| Rapport (phase 2)                  | Médoune, Jérémy    |
