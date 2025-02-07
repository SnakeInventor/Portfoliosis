export type MyShieldProps = {
  text: string;
  image: string;
  color: string;
};

export default function MyShield({ text, image, color }: MyShieldProps) {
  return (
    <div
      className="shield"
      style={{ backgroundColor: color }}
    >
      <div className="shield__logo">
        <img className="shield__logo-image" src={image}></img>
      </div>
      <div className="shield__text">{text}</div>
    </div>
  );
}
