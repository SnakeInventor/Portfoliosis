import React, { useMemo, useState } from 'react'
import enTextData from "../assets/localisation/Contacts/en-us.json";
import ruTextData from "../assets/localisation/Contacts/ru-ru.json";


type Props = {
  language: "ru-ru" | "en-us"
}

type FormData = {
  name: string;
  email: string;
  message: string;
  agreement: boolean;
};
export default function MyContacts({language}: Props) {

  const textData = useMemo(() => (language === "ru-ru") ?  ruTextData : enTextData, [language])

  

  function validateEmail (emailString:string):boolean {
    // eslint-disable-next-line no-useless-escape
    const regexEmailValidator = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/g;
    if(emailString && emailString.match(regexEmailValidator)){
      return true;
    }else{
      return false;
    }
  }

  const [formData, setFormData] = useState<FormData>({
    name: '',
    email: '',
    message: '',
    agreement: false
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value, type } = e.target;
    
    // validate email
    if (name === "email") {
      const target = e.target as HTMLInputElement;
      if (value == "" || validateEmail(value)) {
        target.style.removeProperty("border");
      } else {
        target.style.setProperty("border", "0.15625rem solid red")
      }
    }

    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? (e.target as HTMLInputElement).checked : value
    }));
    
  };

  const shouldEnableButton = ():boolean => {
    const res = formData.agreement
    && formData.name.trim() !== "" 
    && formData.email.trim() !== ""
    && formData.message.trim() !== ""
    && validateEmail(formData.email)    
    return res;
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    // todo
  };

  return (
    <section id='contacts' className='contacts'>
      <div className="contacts__heading">
        <h2 className="contacts__heading-title">{textData.title}</h2>
        <h4 className="contacts__heading-description">{textData.description}</h4>
      </div>
      <div className="contacts__text">
        <div className="contacts__text-left">
          <div className="contacts__text-left-container">
            <p className="contacts__text-left-paragraph contacts__text-left-paragraph--first"
            >{textData.lefttext.firstparagraph}</p>
            <p className="contacts__text-left-paragraph contacts__text-left-paragraph--second">
              <a className='contacts__text-left-link' href={textData.lefttext.secondparagraph.emaillink.href}
              >{textData.lefttext.secondparagraph.emaillink.text}</a>
              {textData.lefttext.secondparagraph.othertext}
            </p>
            <p className="contacts__text-left-paragraph contacts__text-left-paragraph--third">{textData.lefttext.thirdparagraph}</p>
          </div>
        </div>
        {/* FORM */}
        <form className="contacts__form" onSubmit={handleSubmit}>
          <input name='name' 
          type='text' 
          className="contacts__form-input contacts__form-input--name" 
          placeholder={textData.form.namefieldplaceholder}
          onChange={handleInputChange}>
          </input>

          <input name='email' 
          type='email' 
          className="contacts__form-input contacts__form-input--email" 
          placeholder={textData.form.emailfieldplaceholder} 
          onChange={handleInputChange}>
          </input>

          <textarea name='message' 
          className="contacts__form-input contacts__form-input--message" 
          placeholder={textData.form.messagefieldplaceholder} 
          onChange={handleInputChange}>
          </textarea>

          <div className="contacts__form-agreement">
            <input name='agreement'
            type='checkbox'
            className='contacts__form-agreement-checkbox contacts__form-input'
            onChange={handleInputChange}>
            </input>

            <div className="contacts__form-agreement-text">{textData.form.agreementtext}</div>
          </div>

          <button type='submit' 
          className='gradient-button'
          disabled={!shouldEnableButton()}>
            {textData.form.submitbuttontext}
          </button>
        </form>
        <div className="contacts__text-right">
          <ul className="contacts__text-right-list">
          {
            textData.righttext.links.map((link) =>
            <li className="contacts__text-right-list-item" key={link.title}>
              <p className="contacts__text-right-paragraph">{link.title + ":"}</p> 
              <a className="contacts__text-right-link" href={link.href}>{link.text}</a>
            </li>
            )
          }
          </ul>
        </div>
      </div>
    </section>
  )

  
}