<div id="readme-top"></div>
<!-- PROJECT LOGO -->

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

<br />
<div align="center">

  <!--White Background -->
  <!--<a href="https://zugetextet.com">
    <img src="assets/zugetextet_logo_background_white.png" alt="Logo">
  </a>-->

  <!--Font White Bold -->
  <!--<a href="https://zugetextet.com">
    <img src="assets/zugetextet_logo_domain_white.png" width="500" alt="Logo">
  </a>-->

  <!--Font White Normal -->
  <a href="https://zugetextet.com">
    <img src="assets/zugetextet_logo_domain_white_nobold.png" width="500" alt="Logo">
  </a>

  <br/>
  <br/>

  [![.Net][.NET]][.NET-url]
  [![React][React.js]][React-url]
  <a href="https://js.devexpress.com/"><img src="https://tinyurl.com/devextreme" alt="DevExtreme"></a>
  [![Bootstrap][Bootstrap.com]][Bootstrap-url]
  

  <p align="center">
    <br />
    <a href="https://github.com/SYSTAGGmbH/zugetextet/issues">Report Bug</a>
    ·
    <a href="https://github.com/SYSTAGGmbH/zugetextet/issues">Request Feature</a>
  </p>
</div>
<p align="center">
  <b>
    This repository supports editorials handling incoming aricles, through a well presented overview of all articles and a central point of entry
  </b>
</p>


<!-- TABLE OF CONTENTS -->
## Table of Contents

- [About the Project](#about-the-project)
- [Quick Start](#quick-start)
  - [Config Variables](#config-variables)
- [Usage](#usage)
- [Contributing 📚](#contributing)
- [License 📖](#license)
- [Contact 📣](#contact)
- [Acknowledgments 📄](#acknowledgments)


## About The Project

Zugetextet is a web application where the editorials can generate urls for specific topics. With the urls the authors can submit an article.

Zugetextet has a Frontend written in JavaScript, CSS (React) and a Backend witch contains the ASP.NET Core Web Api-Framework.

This project is a study project, which was primarily developed by the interns Cedric Gottschalk and Nehemia Ghebremussie of SYSTAG GmbH. 

## Requirements

To run the project you need to have docker & git installed on the host machine.

## Quick Start

To run this project clone it with git and build the Docker Image with the Dockerfile contained in the repository. After that you will just have to replace some placeholders (surrounded with `[]`) and thats it.

1. Clone the project with git.

```git clone
git clone https://github.com/SYSTAGGmbH/zugetextet.git
```

2. Open project in shell terminal.
3. Build docker image.

```sh
docker build -t [image_tag] ./zugetextet.formulare
```

4. Replace the placeholders and run:

```sh
docker run -dp [local_port]:5000 \
           -v [volume_name]:/app/App_Data/ \
           -e ASPNETCORE_ENVIRONMENT=Production \
           -e Config__FrontendDomain=[http(s)://your_domain] \
           -e Config__InitialUsername=[username] \
           -e Config__InitialPasswordSHA512Hash=[password_sha512_hash] \
           -e Config__TokenSecret=[secret_for_jwt_generation] \
           -e Config__ConditionsOfParticipationUrl=[url] \
           -e Config__ImprintUrl=[url] \
           -e Config__PrivacyPolicyUrl=[url] [image_tag]
```

OR

Replace the placeholders in the command and `.env` file and run:

```sh
docker run -dp [local_port]:5000 -v [volume_name]:/app/App_Data/ --env-file ./.env [image-tag]
```

OR

Replace the placeholders in the `docker-compose.yml` and `.env` file and run:

```sh
docker compose up -d
```

The `FrontendDomain` variable needs to be overwritten by an environment variable, otherwise the URLs pointing to the submission forms wouldn't work. Futhermore the login and authentication needs to be configured with the `InitialUsername`, `InitialPasswordSHA512Hash` and `TokenSecret` variables. The SHA512 hash for the password of your choice can be computed on [sha512.online](https://sha512.online/). The `TokenSecret` can be any string, but for security reasons it should be at least 32 characters long.

Enable the **Production** environment by setting the `ASPNETCORE_ENVIRONMENT` environment variable to **Production**. Otherwise it can result in displaying sensitive information from exceptions to end users.

### Config Variables

To overwrite a config variable with an environment variable the env variable needs to be prefixed with `Config__`

| Variable                             | Description                                                                                                           |
| ------------------------------------ | --------------------------------------------------------------------------------------------------------------------- |
| Version                              | Project version                                                                                                       |
| ConditionsOfParticipationUrl         | Link to the Conditions of Participation                                                                               |
| ImprintUrl                           | Link to the Imprint                                                                                                   |
| PrivacyPolicyUrl                     | Link to the PrivacyPolicy                                                                                             |
| AllowedMimeTypes                     | An array with allowed Mime types for the upload Fields (Note: does not apply for the Image upload field)              | 
| AllowedFileExtensions                | An array with the allowed file extensions for the upload fields (Note: does not apply for the multi-upload of Images) |
| AllowedImageMimeTypes                | An array with allowed Mime types for the multi upload field of Images                                                 |
| AllowedImageFileExtensions           | An array with the allowed file extensions for the multi upload field of Images                                        |
| AllowedParentalConsentMimeTypes      | An array with allowed Mime types for the upload field of the Parental consent                                         |
| AllowedParentalConsentFileExtensions | An array with the allowed file extensions for the upload field of the Parental consent                                |
| MaxFileSize                          | The max file size (per file) for the upload fields                                                                    |
| InitialUsername                      | The initial username for the login                                                                                    |
| InitialPasswordSHA512Hash            | The initial password for the login                                                                                    |
| TokenSecret                          | The value with which the securityKey is generated. The securityKey is required for generating the JWT                 |
| FrontendDomain                       | The domain to reach the frontend                                                                                      |
| SubmitFormHeaderVisible              | A bool value which determines, wheather the header of the submission will be displayed                                |
| SubmitFormFooterVisible              | A bool value which determines, wheather the footer of the submission will be displayed                                |

<!-- USAGE EXAMPLES -->
## Usage

![Overview Submission Forms][submission_forms]  

An Overview of all created submission forms descending by end date.

By clicking the plus icon in the top right corner a new submission form can be created.  
Each of the submission forms can be edited by clicking the edit icon at the end of each row.

<br/>

![Submission Form Editor][submission_form_editor]  

**Name**: Name of the topic  
**Von**: Start date of the submission period  
**Bis**: End date of the submission period  
**Prosatext sichtbar**: Enable the upload of a "Prosatext"  
**Grafiken sichtbar**: Enable the upload of multiple images (max. 5)  
**Max. Anzahl Lyriktexte**: Maximum amount of "Lyriktexts"

<br/>

![Example Overview Submissions][submissions_example]  

An Overview of all submissions.

Each of the columns can be filtered. It is possible to group by columns. The table can be exported to excel (the filters will be applied)


With the url of a submission form, anyone can submit their works (texts and images).



<!-- LICENSE -->
<!-- ToDo: Was für eine Lizenz? -->
## License

Distributed under the GNU General Public License v3.0. See `License.md` in the Licences folder for more information.


<!-- CONTACT -->
## Contact

[SYSTAG GmbH](https://systag.com)



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Cedric Gottschalk](https://github.com/LordFedri)
* [Nehemia Ghebremussie](https://gitlab.com/Nehem1)
* Jochen Salcher (Supervisor)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/SYSTAGGmbH/zugetextet.svg?style=for-the-badge
[contributors-url]: https://github.com/SYSTAGGmbH/zugetextet/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/SYSTAGGmbH/zugetextet.svg?style=for-the-badge
[forks-url]: https://github.com/SYSTAGGmbH/zugetextet/network/members
[stars-shield]: https://img.shields.io/github/stars/SYSTAGGmbH/zugetextet.svg?style=for-the-badge
[stars-url]: https://github.com/SYSTAGGmbH/zugetextet/stargazers
[issues-shield]: https://img.shields.io/github/issues/SYSTAGGmbH/zugetextet.svg?style=for-the-badge
[issues-url]: https://github.com/SYSTAGGmbH/zugetextet/issues
[license-shield]: https://img.shields.io/github/license/SYSTAGGmbH/zugetextet.svg?style=for-the-badge
[license-url]: https://github.com/SYSTAGGmbH/zugetextet/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/linkedin_username
[submissions_example]: assets/submissions_example.png
[submission_form_editor]: assets/submission_form_editor.png
[submission_forms]: assets/submission_forms.png
[DevExtreme]: https://img.shields.io/static/v1?label&message=DevExtreme&color=1b4461&style=for-the-badge&logo=data:image/png;base64,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
[DevExtreme-url]: https://js.devexpress.com/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[.NET]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[.NET-url]: https://dotnet.microsoft.com/en-us/
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
