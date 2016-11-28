package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Core.TeleComSystem;

public class ApplicationRoot {
      public void main() {
        TeleComSystem sys = new TeleComSystem();

        sys.start();
      }

      public void mainIoCContainer() {
        //TODO add container here during the training
        //TODO mention RRR pattern

        TeleComSystem sys = new TeleComSystem();

        sys.start();
      }

    }