import { forwardRef, useImperativeHandle, useRef } from "react";

type MyDialogProps = {
  title: string;
  children?: React.ReactNode;
  buttonParams? : ButtonParams
};

type ButtonParams = {
  buttonText: string;
  onButtonClick?: React.MouseEventHandler<HTMLButtonElement>
}

const MyDialog = forwardRef<HTMLDialogElement, MyDialogProps>(
  ({ title, children, buttonParams}, ref) => {

    const internalRef = useRef<HTMLDialogElement | null>(null);

    useImperativeHandle(ref, () => internalRef.current!, []);

    return (
      <dialog ref={internalRef} className='dialog'>
        <div className="dialog__container">
          <div className="dialog__top">
            <div className="dialog__top-title">{title}</div>
            <button className="dialog__top-close-button"
            onClick={() => {internalRef.current?.close()}}
            >🗙
            </button>
          </div>
          <div className="dialog__middle">{children}</div>
          {
            (buttonParams) &&
            <div className="dialog__bottom">

              <button 
              className="dialog__bottom-button"
              onClick={buttonParams.onButtonClick}>
                {buttonParams.buttonText}
              </button>
            </div>
          }
          
        </div>
      </dialog>
    );
  }
);

export default MyDialog;