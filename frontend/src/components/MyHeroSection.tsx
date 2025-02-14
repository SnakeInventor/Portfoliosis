import { useMemo } from "react";

import logo__snake from "/src/assets/logo__snake.png";
import logo__cog1 from "/src/assets/logo__cog1.png";
import logo__cog2 from "/src/assets/logo__cog2.png";

import enTextData from "../assets/localisation/Hero/en-us.json"
import ruTextData from "../assets/localisation/Hero/ru-ru.json"


type MyHeroSectionProps = {
  language: "ru-ru" | "en-us"
}

export default function MyHeroSection({language}: MyHeroSectionProps) {

  const textData = useMemo(() => (language === "ru-ru") ?  ruTextData : enTextData, [language]);

  return (
    <section id="hero" className="hero">
      <div className="hero__container">
        <div className="hero__logo-container">
          <img className="hero__logo-cog2" src={logo__cog2}></img>
          <img className="hero__logo-cog1" src={logo__cog1}></img>
          <img className="hero__logo-snake" src={logo__snake}></img>  
        </div>

        <div className="hero__text">
          <h1 className="hero__text-heading">{textData.heading}</h1>
          <h3 className="hero__text-underheading">
            {textData.underheading}
          </h3>
          <div className="hero__text-button-wrapper">
            <button className="gradient-button hero__text-button"
            onClick={() => document.getElementById("contacts")?.scrollIntoView({"behavior":"smooth"})}>
              {textData.contactButton}
            </button>
          </div>
        </div>
      </div>
    </section>
  );
}
