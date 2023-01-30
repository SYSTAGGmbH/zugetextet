import useAppMetaData from "../hooks/useAppMetaData";

export default function Footer() {
  const {
    version,
    conditionsOfParticipationUrl,
    imprintUrl,
    privacyPolicyUrl,
  } = useAppMetaData();

  return (
    <footer className="footer d-flex align-items-center px-2 py-2 mt-auto bg-black text-white">
      Version {version}
      <div className="d-flex justify-content-end flex-grow-1">
        <a
          className="p-1"
          href={conditionsOfParticipationUrl}
          alt="Link zu den Teilnahmebedingungen"
        >
          Teilnahmebedingungen
        </a>
        <a className="p-1" href={imprintUrl} alt="Link zum Impressum">
          Impressum
        </a>
        <a
          className="p-1"
          href={privacyPolicyUrl}
          alt="Link zur Datenschutzerklärung"
        >
          Datenschutzerkärung
        </a>
      </div>
    </footer>
  );
}
