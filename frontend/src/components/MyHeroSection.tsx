import logo__snake from "/src/assets/logo__snake.png";
import logo__cog1 from "/src/assets/logo__cog1.png";
import logo__cog2 from "/src/assets/logo__cog2.png";

export default function MyHeroSection() {
  return (
    <section id="hero" className="hero">
      <div className="hero__container">
        <div className="hero__logo-container">
          <img className="hero__logo-cog2" src={logo__cog2}></img>
          <img className="hero__logo-cog1" src={logo__cog1}></img>
          <img className="hero__logo-snake" src={logo__snake}></img>  
        </div>

        <div className="hero__text">
          <h1 className="hero__text-heading">SNAKEINVENTOR</h1>
          <h3 className="hero__text-underheading">
            Building things like no other
          </h3>
          <div className="hero__text-button-wrapper">
            <button className="gradient-button">Get in touch</button>
          </div>
        </div>
      </div>
    </section>
  );
}
