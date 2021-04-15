export function setLightMode() {
  appendStyle("./_content/tomi.arcade.client.settings/light.css");
}

export function setDarkMode() {
  appendStyle("./_content/tomi.arcade.client.settings/dark.css");
}

export function appendStyle(path) {
  var element = document.getElementById("theme-link");
  element.setAttribute("href", path);
}