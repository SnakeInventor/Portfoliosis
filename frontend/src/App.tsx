import { useEffect, useState } from "react";
import "./App.scss";
import MyHeader from "./components/MyHeader";
import MyHeroSection from "./components/MyHeroSection";
import MySkills from "./components/MySkills";
import MyProjects from "./components/MyProjects";
import MyContacts from "./components/MyContacts";
function App() {
  const [currentLanguage, setCurrentLanguage] = useState<"en-us"|"ru-ru">("en-us")
  const toggleLanguage = () => {
    setCurrentLanguage(currentLanguage === "en-us" ? "ru-ru" : "en-us");
  };

  useEffect(() => {
    const userLanguage = navigator.language.toLowerCase();
    if (userLanguage.startsWith("ru")) {
      setCurrentLanguage("ru-ru");
    }
  }, []);

  return (
    <>
      <MyHeader language={currentLanguage} onLanguageButtonClick={toggleLanguage}/>
      <MyHeroSection language={currentLanguage}/>
      <MySkills language={currentLanguage}/>
      <MyProjects language={currentLanguage}/>
      <MyContacts language={currentLanguage}/>
    </>
  );
}

export default App;
