import notify from "devextreme/ui/notify";

function notifyFailure(messageStr, duration = 3000) {
  notify(
    {
      message: messageStr,
      position: {
        my: "center top",
        at: "center top",
      },
    },
    "error",
    duration
  );
}

function notifySuccess(messageStr, duration = 3000) {
  notify(
    {
      message: messageStr,
      position: {
        my: "center top",
        at: "center top",
      },
    },
    "success",
    duration
  );
}

export { notifyFailure, notifySuccess };
