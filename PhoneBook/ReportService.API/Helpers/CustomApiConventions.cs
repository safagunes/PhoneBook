using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ReportService.Domain.Core.ResponseBases;

namespace ReportService.API.Helpers
{
    public static class CustomApiConventions
    {
        #region GET
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        { }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Find(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        { }
        #endregion
        //TODO:Postlar sadece güncelleme için olanlara eklenecek. Ekleme olanlar için Create ön eki kullanılarak şekilde kod revize edilecek.
        #region POST
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseOfValidation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Post(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model)
        { }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseOfValidation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Create(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model)
        { }
        #endregion

        #region PUT
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseOfValidation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Put(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id,

            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model)
        { }

        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseOfValidation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Edit(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id,

            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model)
        { }

        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ResponseOfValidation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Update(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id,

            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model)
        { }
        #endregion

        #region DELETE
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Delete(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        { }
        #endregion

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseOfException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseOfException))]
        //[ProducesDefaultResponseType]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Send(
           [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object model)
        { }
    }
}
