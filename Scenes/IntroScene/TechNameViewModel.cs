using Microsoft.Xna.Framework;
using MurderMystery.Main;
using MurderMystery.Models;
using MurderMystery.SceneObjects.Widgets;
using MurderMystery.Scenes.StatusScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MurderMystery.Scenes.IntroScene
{
    public class TechNameViewModel : ViewModel
    {
        Textbox namingBox;

        int confirmCooldown = 100;

        SelectionViewModel.Selection selection;

        public TechNameViewModel(Scene iScene, GameView viewName, SelectionViewModel.Selection iSelection)
            : base(iScene, PriorityLevel.MenuLevel, viewName)
        {
            namingBox = GetWidget<Textbox>("NamingBox");
            selection = iSelection;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Input.CurrentInput.CommandPressed(Command.Cancel))
            {
                //Terminate();
            }
            else if (Input.CurrentInput.CommandPressed(Command.Confirm) && confirmCooldown <= 0 && GetWidget<Button>("OK").Enabled)
            {
                GetWidget<Button>("OK").RadioSelect();
                namingBox.Active = false;
            }
            else if (Input.CurrentInput.CommandReleased(Command.Confirm) && confirmCooldown <= 0 && GetWidget<Button>("OK").Enabled)
            {
                Audio.PlaySound(GameSound.menu_select);
                GetWidget<Button>("OK").UnSelect();
                Proceed();
            }

            if (confirmCooldown > 0) confirmCooldown -= gameTime.ElapsedGameTime.Milliseconds;
        }

        public void Proceed()
        {
            namingBox.Active = false;

            if (selection == SelectionViewModel.Selection.Technology)
            {
                GameProfile.SetSaveData<bool>("NewTechGame", true);
                GameProfile.SetSaveData<bool>("NoviceWarriorRecruitable", true);
                GameProfile.SetSaveData<bool>("NoviceMageRecruitable", true);

                var hero = new HeroModel(HeroType.TechHero);
                hero.Name.Value = namingBox.Text;
                GameProfile.PlayerProfile.Party.Add(hero);

                GameProfile.SetSaveData<HeroModel>("PartyLeader", hero);
                GameProfile.SetSaveData<string>("WindowStyle", GameProfile.PlayerProfile.WindowStyle.Value);
                GameProfile.SetSaveData<GameFont>("Font", GameProfile.PlayerProfile.Font.Value);

                CrossPlatformGame.Transition(typeof(MapScene.MapScene), "HomeLab", 5, 7, SceneObjects.Maps.Orientation.Up);
            }
            else
            {
                GameProfile.SetSaveData<bool>("NewMagicGame", true);

                var hero = new HeroModel(HeroType.TechnoMage);
                hero.Name.Value = namingBox.Text;
                GameProfile.PlayerProfile.Party.Add(hero);

                GameProfile.SetSaveData<HeroModel>("PartyLeader", hero);
                GameProfile.SetSaveData<string>("WindowStyle", GameProfile.PlayerProfile.WindowStyle.Value);
                GameProfile.SetSaveData<GameFont>("Font", GameProfile.PlayerProfile.Font.Value);

                CrossPlatformGame.Transition(typeof(MapScene.MapScene), "MageConvention", 14, 9, SceneObjects.Maps.Orientation.Up);
            }
        }

        public void ValidateName(Textbox textbox)
        {
            if (string.IsNullOrEmpty(textbox.Text)) GetWidget<Button>("OK").Enabled = false;
            else GetWidget<Button>("OK").Enabled = true;
        }

        public ModelProperty<bool> ReadyToProceed { get; set; } = new ModelProperty<bool>(false);
    }
}
