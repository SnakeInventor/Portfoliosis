import { useState } from "react";
import "./App.scss";
import MyHeader from "./components/MyHeader";
import MyHeroSection from "./components/MyHeroSection";
import MySkills from "./components/MySkills";
import MyProjects from "./components/MyProjects";
function App() {
  const [currentLanguage] = useState<"en-us"|"ru-ru">("en-us")

  return (
    <>
      <MyHeader language={currentLanguage}/>
      <MyHeroSection language={currentLanguage}/>
      <MySkills language={currentLanguage}/>
      <MyProjects language={currentLanguage}/>
    </>
  );
}

export default App;
