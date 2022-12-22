﻿using Microsoft.Xna.Framework;
using MurderMystery.Scenes.StatusScene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MurderMystery.Models
{
    [Serializable]
    public class PlayerProfile
    {
        public PlayerProfile()
        {

        }

        public ModelProperty<string> WindowStyle { get; set; } = new ModelProperty<string>("Blank");
        public ModelProperty<string> FrameStyle { get; set; } = new ModelProperty<string>("Blank");
        public ModelProperty<string> SelectedStyle { get; set; } = new ModelProperty<string>("Blank");
        public ModelProperty<string> FrameSelectedStyle { get; set; } = new ModelProperty<string>("Blank");
        public ModelProperty<string> LabelStyle { get; set; } = new ModelProperty<string>("Blank");
        public ModelProperty<GameFont> Font { get; set; } = new ModelProperty<GameFont>(GameFont.Tooltip);

        public ModelCollection<HeroModel> Party { get; set; } = new ModelCollection<HeroModel>();
        public ModelProperty<int> Money { get; set; } = new ModelProperty<int>(50);

    }
}
