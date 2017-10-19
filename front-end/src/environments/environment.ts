// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
    production: false,
    apiUrl: 'http://localhost:6969/api/',
    firebase: {
        apiKey: 'AIzaSyBBR1NS7bn5onuK2achpioXWqWBBVUYwhs',
        authDomain: 'seac-digit-template.firebaseapp.com',
        databaseURL: 'https://seac-digit-template.firebaseio.com',
        projectId: 'seac-digit-template',
        storageBucket: 'seac-digit-template.appspot.com',
        messagingSenderId: '95363734924'
    }
};
